using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AdDesignDataContainer
		: ListDataContainer<AdDesignDataContainer, AdDesignData>
	{
		public AdDesignData FindByUserDBKey(DBKey userDBKey)
		{
			return Find(item => item.UserDBKey == userDBKey);
		}

		public AdDesignData FindByCampaignDBKey(DBKey campaignDBKey)
		{
			return Find(item => item.CampaignDBKey == campaignDBKey);
		}

		public AdDesignData FindByAdDesignDBKey(DBKey adDesignDBKey)
		{
			return Find(item => item.AdDesignDBKey == adDesignDBKey);
		}
	}
}
