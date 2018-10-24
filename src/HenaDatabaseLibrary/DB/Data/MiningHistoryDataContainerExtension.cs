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
    public static class MiningHistoryDataContainerExtension
	{
		#region Items
		private static async Task<int> FromDBByIdAsync<T>(MiningHistoryDataContainer item, DBKey id, int offset = 0, int limit = 20)
			where T : DBQuery_MiningHistory_Select_Base<COMMON_IN_DATA_DBKeyOffsetLimit>, new()
		{
			var query = new T();
			query.IN.DBKey = id;
			query.IN.Offset = offset;
			query.IN.Limit = limit;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByCustomerIdAsync(this MiningHistoryDataContainer item, DBKey userId, int offset = 0, int limit = 20)
		{
			return await FromDBByIdAsync<DBQuery_MiningHistory_Select_By_UserId>(item, userId, offset, limit);
		}
		#endregion // Items
	}
}
