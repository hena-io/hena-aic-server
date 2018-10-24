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
using HenaWebsite.Models.API.User;
using HenaWebsite.Models.API.UserService;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("service/users/[action]")]
	public class UserServiceAPI : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 잔고 조회
		[HttpPost]
		public async Task<IActionResult> Balances([FromBody] UserServiceModels.Balances.Request request)
		{
			if( request.UserId == GlobalDefine.INVALID_DBKEY )
				return APIResponse(ErrorCode.InvalidParameters);

			UserBasicData userBasicData = new UserBasicData();
			if( await userBasicData.FromDBAsync(request.UserId) == false )
				return APIResponse(ErrorCode.InvalidId);

			BalanceDataContainer balances = new BalanceDataContainer();
			await balances.FromDBByUserIdAsync(request.UserId);

			var currencies = Enum.GetValues(typeof(CurrencyTypes));
			foreach( CurrencyTypes it in currencies )
			{
				if (it == CurrencyTypes.None)
					continue;

				balances.FindOrAdd(request.UserId, it);
			}

			var response = new UserServiceModels.Balances.Response();
			response.Balances.AddRangeSafe(balances.Items);
			return Success(response);
		}
		
		// -------------------------------------------------------------------------------
		// AIC 기록
		[HttpPost]
		public async Task<IActionResult> AICHistory([FromBody] UserServiceModels.AICHistory.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			UserBasicData userBasicData = new UserBasicData();
			if (await userBasicData.FromDBAsync(request.UserId) == false)
				return APIResponse(ErrorCode.InvalidId);

			if( request.Count <= 0 )
			{
				request.Count = 20;
			}

			AdHistoryDataContainer histories = new AdHistoryDataContainer();
			if (request.IsPublisherReport)
			{
				await histories.FromDBByPublisherIdAsync(request.UserId, request.Offset, request.Count);
			}
			else
			{
				await histories.FromDBByCustomerIdAsync(request.UserId, request.Offset, request.Count);
			}

			var response = new UserServiceModels.AICHistory.Response();
			response.Items.AddRangeSafe(histories.Items);
			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// AIC 리포트
		[HttpPost]
		public async Task<IActionResult> AICRevenueReport([FromBody] UserServiceModels.AICRevenueReport.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			UserBasicData userBasicData = new UserBasicData();
			if (await userBasicData.FromDBAsync(request.UserId) == false)
				return APIResponse(ErrorCode.InvalidId);

			RevenueReportDataContainer revenueReports = new RevenueReportDataContainer();
			if( request.IsPublisherReport )
			{
				await revenueReports.FromDBByPublisherIdAsync(request.UserId, request.BeginTime, request.EndTime, request.TimeZoneOffset);
			}
			else
			{
				await revenueReports.FromDBByCustomerIdAsync(request.UserId, request.BeginTime, request.EndTime, request.TimeZoneOffset);
			}

			var response = new UserServiceModels.AICRevenueReport.Response();
			response.Items.AddRangeSafe(revenueReports.Items);
			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// Mining 기록
		[HttpPost]
		public async Task<IActionResult> MiningHistory([FromBody] UserServiceModels.Balances.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			UserBasicData userBasicData = new UserBasicData();
			if (await userBasicData.FromDBAsync(request.UserId) == false)
				return APIResponse(ErrorCode.InvalidId);

			BalanceDataContainer balances = new BalanceDataContainer();
			await balances.FromDBByUserIdAsync(request.UserId);

			var currencies = Enum.GetValues(typeof(CurrencyTypes));
			foreach (CurrencyTypes it in currencies)
			{
				balances.FindOrAdd(request.UserId, it);
			}

			var response = new UserServiceModels.Balances.Response();
			response.Balances.AddRangeSafe(balances.Items);
			return Success(response);
		}
		#endregion // API

	}
}