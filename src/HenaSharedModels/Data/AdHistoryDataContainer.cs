using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AdHistoryDataContainer
		: ListDataContainer<AdHistoryDataContainer, AdHistoryData>
	{
		#region Find
		public AdHistoryData FindByAdHistoryId(DBKey adHistoryId)
		{
			return Find(item => item.AdHistoryId == adHistoryId);
		}
		public AdHistoryData FindByPublisherId(DBKey publisherId)
		{
			return Find(item => item.PublisherId == publisherId);
		}
		public AdHistoryData FindByAppId(DBKey appId)
		{
			return Find(item => item.AppId == appId);
		}
		public AdHistoryData FindByAdUnitId(DBKey adUnitId)
		{
			return Find(item => item.AdUnitId == adUnitId);
		}
		public AdHistoryData FindByAdvertiserId(DBKey advertiserId)
		{
			return Find(item => item.AdvertiserId == advertiserId);
		}
		public AdHistoryData FindByCampaignId(DBKey campaignId)
		{
			return Find(item => item.CampaignId == campaignId);
		}
		public AdHistoryData FindByAdDesignId(DBKey adDesignId)
		{
			return Find(item => item.AdDesignId == adDesignId);
		}
		#endregion // Find

		#region FindAll
		public List<AdHistoryData> FindAllByAdHistoryId(DBKey adHistoryId)
		{
			return FindAll(item => item.AdHistoryId == adHistoryId);
		}
		public List<AdHistoryData> FindAllByPublisherId(DBKey publisherId)
		{
			return FindAll(item => item.PublisherId == publisherId);
		}
		public List<AdHistoryData> FindAllByAppId(DBKey appId)
		{
			return FindAll(item => item.AppId == appId);
		}
		public List<AdHistoryData> FindAllByAdUnitId(DBKey adUnitId)
		{
			return FindAll(item => item.AdUnitId == adUnitId);
		}
		public List<AdHistoryData> FindAllByAdvertiserId(DBKey advertiserId)
		{
			return FindAll(item => item.AdvertiserId == advertiserId);
		}
		public List<AdHistoryData> FindAllByCampaignId(DBKey campaignId)
		{
			return FindAll(item => item.CampaignId == campaignId);
		}
		public List<AdHistoryData> FindAllByAdDesignId(DBKey adDesignId)
		{
			return FindAll(item => item.AdDesignId == adDesignId);
		}
		#endregion // FindAll
	}
}
