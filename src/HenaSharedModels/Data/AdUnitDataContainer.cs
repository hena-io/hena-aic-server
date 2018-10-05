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
        public AdUnitData FindByUserDBKey(DBKey userDBKey)
		{
			return Find(item => item.UserDBKey == userDBKey);
		}

		public AdUnitData FindByAppDBKey(DBKey appDBKey)
		{
			return Find(item => item.AppDBKey == appDBKey);
		}

		public AdUnitData FindByAdUnitDBKey(DBKey adUnitDBKey)
		{
			return Find(item => item.AdUnitDBKey == adUnitDBKey);
		}
	}
}
