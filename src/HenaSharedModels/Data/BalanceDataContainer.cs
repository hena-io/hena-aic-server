using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class BalanceDataContainer
		: ListDataContainer<BalanceDataContainer, BalanceData>
	{
		public BalanceData Find(DBKey userId, CurrencyTypes currencyType)
		{
			return Find(item => item.UserId == userId && item.CurrencyType == currencyType);
		}

		public BalanceData FindOrAdd(DBKey userId, CurrencyTypes currencyType)
		{
			var balance = Find(userId, currencyType);
			if( balance == null)
			{
				balance = new BalanceData()
				{
					UserId = userId,
					CurrencyType = currencyType,
					Balance = 0m,
					CreateTime = DateTime.UtcNow,
					LastUpdate = DateTime.UtcNow,
				};
				Add(balance);
			}
			return balance;
		}

		public List<BalanceData> FindAll(DBKey userId)
		{
			return FindAll(item => item.UserId == userId);
		}

		public List<BalanceData> FindAll(CurrencyTypes currencyType)
		{
			return FindAll(item => item.CurrencyType == currencyType);
		}
	}
}
