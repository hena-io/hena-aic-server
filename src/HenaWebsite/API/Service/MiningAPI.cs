using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Shared.Data;
using HenaWebsite.Mining;
using HenaWebsite.Models;
using HenaWebsite.Models.API;
using HenaWebsite.Models.API.MiningModel;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("service/mining/[action]")]
	public class MiningAPI : BaseApi
	{
		#region API

		// -------------------------------------------------------------------------------
		// Mining 시작
		[HttpPost]
		public async Task<IActionResult> MiningStart([FromBody] MiningModels.MiningStart.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			if (await MiningManager.Instance.StartMining(request.UserId) == false)
				return APIResponse(ErrorCode.Failed);

			return Success();
		}

		// -------------------------------------------------------------------------------
		// Mining 정지
		[HttpPost]
		public IActionResult MiningStop([FromBody] MiningModels.MiningStart.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			if (MiningManager.Instance.IsRunning(request.UserId) == false)
				return APIResponse(ErrorCode.NotRunning);

			MiningManager.Instance.StopMining(request.UserId);
			return Success();
		}

		// -------------------------------------------------------------------------------
		// Mining 세션 업데이트
		[HttpPost]
		public IActionResult MiningUpdateSession([FromBody] MiningModels.UpdateSession.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			if (MiningManager.Instance.UpdateSession(request.UserId) == false)
				return APIResponse(ErrorCode.NotRunning);

			return Success();
		}


		// -------------------------------------------------------------------------------
		// Mining 기록
		[HttpPost]
		public async Task<IActionResult> MiningHistory([FromBody] MiningModels.MiningHistory.Request request)
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

			MiningHistoryDataContainer histories = new MiningHistoryDataContainer();
			await histories.FromDBByCustomerIdAsync(request.UserId, request.Offset, request.Count);

			var response = new MiningModels.MiningHistory.Response();
			response.Items.AddRangeSafe(histories.Items);
			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// Mining 리포트
		[HttpPost]
		public async Task<IActionResult> MiningRevenueReport([FromBody] MiningModels.MiningRevenueReport.Request request)
		{
			if (request.UserId == GlobalDefine.INVALID_DBKEY)
				return APIResponse(ErrorCode.InvalidParameters);

			UserBasicData userBasicData = new UserBasicData();
			if (await userBasicData.FromDBAsync(request.UserId) == false)
				return APIResponse(ErrorCode.InvalidId);

			RevenueReportDataContainer revenueReports = new RevenueReportDataContainer();
			await revenueReports.FromDBByCustomerIdAsync(request.UserId, request.BeginTime, request.EndTime, request.TimeZoneOffset);

			var response = new MiningModels.MiningRevenueReport.Response();
			response.Items.AddRangeSafe(revenueReports.Items);
			return Success(response);
		}
		#endregion // API

	}
}