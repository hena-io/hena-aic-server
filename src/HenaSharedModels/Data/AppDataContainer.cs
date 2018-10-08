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
        public AppData FindByUserId(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public AppData FindByAppId(DBKey campaignId)
		{
			return Find(item => item.AppId == campaignId);
		}
	}
}
