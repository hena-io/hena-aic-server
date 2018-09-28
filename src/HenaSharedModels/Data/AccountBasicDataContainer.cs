using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AccountBasicDataContainer
		: ListDataContainer<AccountBasicDataContainer, AccountBasicData>
	{
        public AccountBasicData FindByAccountDBKey(DBKey accountDBKey)
		{
			lock(Items)
			{
				return Items.Find(item => item.AccountDBKey == accountDBKey);
			}
		}
	}
}
