using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AppDataContainer
		: ListDataContainer<AppDataContainer, AppData>
	{
        public AppData FindByUserDBKey(DBKey userDBKey)
		{
			return Find(item => item.UserDBKey == userDBKey);
		}

		public AppData FindByAppDBKey(DBKey campaignDBKey)
		{
			return Find(item => item.AppDBKey == campaignDBKey);
		}
	}
}
