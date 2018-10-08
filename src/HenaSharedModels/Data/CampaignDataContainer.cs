using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class CampaignDataContainer
		: ListDataContainer<CampaignDataContainer, CampaignData>
	{
        public CampaignData FindByUserId(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public CampaignData FindByCampaignId(DBKey campaignId)
		{
			return Find(item => item.CampaignId == campaignId);
		}
	}
}
