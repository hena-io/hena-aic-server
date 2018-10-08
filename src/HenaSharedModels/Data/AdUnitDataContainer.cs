using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AdUnitDataContainer
		: ListDataContainer<AdUnitDataContainer, AdUnitData>
	{
        public AdUnitData FindByUserId(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public AdUnitData FindByAppId(DBKey appId)
		{
			return Find(item => item.AppId == appId);
		}

		public AdUnitData FindByAdUnitId(DBKey adUnitId)
		{
			return Find(item => item.AdUnitId == adUnitId);
		}
	}
}
