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
		private static async Task<int> FromDBByIdAsync<T>(AdHistoryDataContainer item, DBKey id)
			where T : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>, new()
		{
			var query = new DBQuery_AdHistory_Select_By_CampaignId();
			query.IN.DBKey = id;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByPublisherIdAsync(this AdHistoryDataContainer item, DBKey publisherId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_PublisherId>(item, publisherId);
		}
		
		public static async Task<int> FromDBByAdvertiserIdAsync(this AdHistoryDataContainer item, DBKey advertiserId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdvertiserId>(item, advertiserId);
		}
		
		public static async Task<int> FromDBByAppIdAsync(this AdHistoryDataContainer item, DBKey appId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AppId>(item, appId);
		}
		
		public static async Task<int> FromDBByAdUnitIdAsync(this AdHistoryDataContainer item, DBKey adUnitId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdUnitId>(item, adUnitId);
		}
		
		public static async Task<int> FromDBByCampaignIdAsync(this AdHistoryDataContainer item, DBKey campaignId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_CampaignId>(item, campaignId);
		}
		
		public static async Task<int> FromDBByAdDesignIdAsync(this AdHistoryDataContainer item, DBKey adDesignId)
		{
			return await FromDBByIdAsync<DBQuery_AdHistory_Select_By_AdDesignId>(item, adDesignId);
		}
		#endregion // Items
	}
}
