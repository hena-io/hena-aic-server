using Hena;
using Hena.DB;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Mining
{

	public class MiningData
	{
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		public DateTime NextMiningTime { get; set; } = DateTime.MinValue;
		public DateTime LastMiningTime { get; set; } = DateTime.MinValue;

		public DateTime ExpireTime { get; set; } = DateTime.MinValue;

		public void RefreshSessionExpireTime()
		{
			ExpireTime = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1));
		}
	}

	public class MiningManager : SingletonThreadService<MiningManager>
	{
		Range<TimeSpan> MiningTimeRange = new Range<TimeSpan>(TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(1));
		Range<decimal> MiningAmountRange = new Range<decimal>(0.0000001m, 0.001m);
		Dictionary<DBKey, MiningData> Items { get; set; } = new Dictionary<DBKey, MiningData>();

		public async Task<bool> StartMining(DBKey userId)
		{
			if (userId == GlobalDefine.INVALID_DBKEY)
				return false;

			UserBasicData userBasicData = new UserBasicData();
			if (await userBasicData.FromDBAsync(userId) == false)
				return false;

			lock(Items)
			{
				if (Items.ContainsKey(userId))
					return false;

				var miningData = new MiningData()
				{
					UserId = userId,
					NextMiningTime = DateTime.UtcNow.AddSeconds(RandomEx.Range(MiningTimeRange.min.TotalSeconds, MiningTimeRange.max.TotalSeconds)),
					LastMiningTime = DateTime.UtcNow,
				};
				miningData.RefreshSessionExpireTime();

				Items.Add(userId, miningData);
			}
			return true;
		}

		public bool IsRunning(DBKey userId)
		{
			return Items.ContainsKey_LockThis(userId);
		}

		public void StopMining(DBKey userId)
		{
			Items.Remove_LockThis(userId);
		}

		public bool UpdateSession(DBKey userId)
		{
			MiningData miningData;
			if( Items.TryGetValueEx_LockThis(userId, out miningData) )
			{
				miningData.RefreshSessionExpireTime();
				return true;
			}
			return false;
		}

		#region IService
		protected override void OnBeginService()
		{
			UpdateIntervalMS = (long)TimeSpan.FromSeconds(5).TotalMilliseconds;
		}
		protected override void OnUpdateService(double deltaTimeSec)
		{
			if (Items.Count_LockThis() == 0)
				return;

			var utcNow = DateTime.UtcNow;
			var items = Items.ToArrayValue_LockThis();

			List<DBQuery> queries = new List<DBQuery>();

			foreach (var it in items)
			{
				if (utcNow >= it.NextMiningTime)
				{
					it.LastMiningTime = utcNow;
					it.NextMiningTime = utcNow.AddSeconds(RandomEx.Range(MiningTimeRange.min.TotalSeconds, MiningTimeRange.max.TotalSeconds));

					var miningAmount = RandomEx.Range(MiningAmountRange.min, MiningAmountRange.max);

					var miningHistoryQuery = new DBQuery_MiningHistory_Insert();
					var item = miningHistoryQuery.IN.Item;
					item.MiningHistoryId = IDGenerator.NewMiningHistoryId;
					item.UserId = it.UserId;
					item.CurrencyType = CurrencyTypes.HENA_MINING;
					item.MiningAmount = miningAmount;
					item.MiningTime = utcNow;
					queries.Add(miningHistoryQuery);

					var balanceAddQuery = new DBQuery_Balance_Add();
					balanceAddQuery.IN.UserId = it.UserId;
					balanceAddQuery.IN.CurrencyType = CurrencyTypes.HENA_MINING;
					balanceAddQuery.IN.Amount = miningAmount;
					queries.Add(balanceAddQuery);
				}

				if (utcNow > it.ExpireTime)
				{
					StopMining(it.UserId);
				}
			}

			DBThread.Instance.ReqQuery(queries.ToArray());

		}
		protected override void OnEndService()
		{
		}
		#endregion // IService

	}
}
