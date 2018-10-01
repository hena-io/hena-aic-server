using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class UserBasicDataContainer
		: ListDataContainer<UserBasicDataContainer, UserBasicData>
	{
        public UserBasicData FindByUserDBKey(DBKey userDBKey)
		{
			lock(Items)
			{
				return Items.Find(item => item.UserDBKey == userDBKey);
			}
		}
	}
}
