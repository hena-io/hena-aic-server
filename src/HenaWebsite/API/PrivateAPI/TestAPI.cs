using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.API;
using HenaWebsite.Models.API.Test;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class TestAPI : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 입금
		[HttpPost]
		public async Task<IActionResult> Deposit([FromBody] TestModels.Deposit.Request request)
		{
			if (request == null || request.CurrencyType == CurrencyTypes.None || request.Amount == 0m)
				return APIResponse(ErrorCode.InvalidParameters);

			DBQuery_Balance_Add balanceAddQuery = new DBQuery_Balance_Add();
			balanceAddQuery.IN.UserId = UserId;
			balanceAddQuery.IN.CurrencyType = request.CurrencyType;
			balanceAddQuery.IN.Amount = request.Amount;

			if (await DBThread.Instance.ReqQueryAsync(balanceAddQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);
			
			var response = new TestModels.Deposit.Response();
			response.Amount = balanceAddQuery.OUT.Balance.Amount;
			response.CurrencyType = balanceAddQuery.OUT.Balance.CurrencyType;
			return Success(response);
		}
		#endregion	// API



		#region Internal Methods

		#endregion // Internal Methods



	}
}