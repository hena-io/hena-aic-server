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
        public CampaignData FindByUserDBKey(DBKey userDBKey)
		{
			return Find(item => item.UserDBKey == userDBKey);
		}

		public CampaignData FindByCampaignDBKey(DBKey campaignDBKey)
		{
			return Find(item => item.CampaignDBKey == campaignDBKey);
		}
	}
}
