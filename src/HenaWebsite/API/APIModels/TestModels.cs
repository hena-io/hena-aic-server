using Hena;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.Test
{
	public static class TestModels
	{
		// 테스트 입금
		public static class Deposit
		{
			public class Request
			{
				public CurrencyTypes CurrencyType = CurrencyTypes.None;
				public decimal Amount { get; set; } = 0m;
			}

			public class Response
			{
				public CurrencyTypes CurrencyType = CurrencyTypes.None;
				public decimal Amount { get; set; } = 0m;				// 입금 후 잔량
			}
		}
		
	}
}
