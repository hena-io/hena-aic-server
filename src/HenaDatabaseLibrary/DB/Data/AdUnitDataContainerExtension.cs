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
    public static class AdUnitDataContainerExtension
	{
		public static async Task<int> FromDBByUserIdAsync(this AdUnitDataContainer item, DBKey userId)
		{
			var query = new DBQuery_AdUnit_Select_By_UserId();
			query.IN.DBKey = userId;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByAppIdAsync(this AdUnitDataContainer item, DBKey appId)
		{
			var query = new DBQuery_AdUnit_Select_By_AppId();
			query.IN.DBKey = appId;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
	}
}
