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
    public static class AdDesignDataContainerExtension
	{
		public static async Task<int> FromDBByUserDBKeyAsync(this AdDesignDataContainer item, DBKey userDBKey)
		{
			var query = new DBQuery_AdDesign_Select_By_UserDBKey();
			query.IN.DBKey = userDBKey;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByCampaignDBKeyAsync(this AdDesignDataContainer item, DBKey campaignDBKey)
		{
			var query = new DBQuery_AdDesign_Select_By_CampaignDBKey();
			query.IN.DBKey = campaignDBKey;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
	}
}
