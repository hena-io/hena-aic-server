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
		public AdDesignData FindByUserId(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public AdDesignData FindByCampaignId(DBKey campaignId)
		{
			return Find(item => item.CampaignId == campaignId);
		}

		public AdDesignData FindByAdDesignId(DBKey adDesignId)
		{
			return Find(item => item.AdDesignId == adDesignId);
		}
	}
}
