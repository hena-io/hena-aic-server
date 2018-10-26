using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena.DB;
using Hena.Shared.Data;
using HenaWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Authorize]
    public class TestController : BaseController
	{
		[HttpGet]
		public IActionResult Deposit()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Deposit(CurrencyTypes currencyType = CurrencyTypes.None, decimal amount = 0m)
		{
			if (currencyType == CurrencyTypes.None || amount == 0m)
			{
				return ViewWithErrorMessage("error", "Invalid parameters");
			}

			DBQuery_Balance_Add balanceAddQuery = new DBQuery_Balance_Add();
			balanceAddQuery.IN.UserId = UserId;
			balanceAddQuery.IN.CurrencyType = currencyType;
			balanceAddQuery.IN.Amount = amount;

			if (await DBThread.Instance.ReqQueryAsync(balanceAddQuery) == false)
				return ViewWithErrorMessage("error", "Failed");

			return View();
		}
	}
}