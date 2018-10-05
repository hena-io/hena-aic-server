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
    public static class CampaignDataContainerExtension
	{
		public static async Task<int> FromDBByUserDBKeyAsync(this CampaignDataContainer item, DBKey userDBKey)
		{
			var query = new DBQuery_Campaign_Select_By_UserDBKey();
			query.IN.DBKey = userDBKey;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
	}
}
