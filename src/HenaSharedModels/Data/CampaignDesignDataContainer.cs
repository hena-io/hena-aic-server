using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class CampaignDesignDataContainer
		: ListDataContainer<CampaignDesignDataContainer, CampaignDesignData>
	{
		public CampaignDesignData FindByUserDBKey(DBKey userDBKey)
		{
			return Find(item => item.UserDBKey == userDBKey);
		}

		public CampaignDesignData FindByCampaignDBKey(DBKey campaignDBKey)
		{
			return Find(item => item.CampaignDBKey == campaignDBKey);
		}

		public CampaignDesignData FindByDesignDBKey(DBKey designDBKey)
		{
			return Find(item => item.DesignDBKey == designDBKey);
		}
	}
}
