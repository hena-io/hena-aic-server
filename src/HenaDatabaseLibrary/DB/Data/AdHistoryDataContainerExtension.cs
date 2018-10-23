using Hena.DB;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
    public static class AdHistoryDataContainerExtension
	{
		#region Items
		private static async Task<int> FromDBByIdAsync<T>(AdHistoryDataContainer item, DBKey id, int offset = 0, int limit = 20)
			where T : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOffsetLimit>, new()
		{
			var query = new DBQuery_AdHistory_Select_By_CampaignId();
			query.IN.DBKey = id;
			query.IN.Offset = offset;
			query.IN.Limit = limit;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByCustomerIdAsync(this AdHistoryDataContainer item, DBKey userId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_CustomerId>(item, userId, offset, limit);
		}

		public static async Task<int> FromDBByPublisherIdAsync(this AdHistoryDataContainer item, DBKey userId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_PublisherId>(item, userId, offset, limit);
		}
		
		public static async Task<int> FromDBByAdvertiserIdAsync(this AdHistoryDataContainer item, DBKey userId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdvertiserId>(item, userId, offset, limit);
		}
		
		public static async Task<int> FromDBByAppIdAsync(this AdHistoryDataContainer item, DBKey appId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AppId>(item, appId, offset, limit);
		}
		
		public static async Task<int> FromDBByAdUnitIdAsync(this AdHistoryDataContainer item, DBKey adUnitId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdUnitId>(item, adUnitId, offset, limit);
		}
		
		public static async Task<int> FromDBByCampaignIdAsync(this AdHistoryDataContainer item, DBKey campaignId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_CampaignId>(item, campaignId, offset, limit);
		}
		
		public static async Task<int> FromDBByAdDesignIdAsync(this AdHistoryDataContainer item, DBKey adDesignId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdDesignId>(item, adDesignId, offset, limit);
		}
		#endregion // Items
	}
}
