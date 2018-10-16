using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Library.Extensions;
using Hena.Security.Claims;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.API.AdUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class AdUnits : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 광고 유닛 생성
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AdUnitModels.Create.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check validation
			AppData appData = new AppData();
			if (await appData.FromDBAsync(request.AppId) == false)
				return APIResponse(ErrorCode.InvalidParameters);

			if (appData.UserId != UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Insert to db
			var insertQuery = new DBQuery_AdUnit_Insert();
			var item = insertQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			item.AdUnitId = IDGenerator.NewAdUnitId;
			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdUnitModels.Create.Response();
			if (await response.FromDBAsync(item.AdUnitId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 유닛 수정
		[HttpPost]
		public async Task<IActionResult> Modify([FromBody] AdUnitModels.Modify.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey adUnitId = request.AdUnitId;
			AdUnitData adUnitData = new AdUnitData();

			// Check validation
			if (await adUnitData.FromDBAsync(adUnitId) == false)
				return APIResponse(ErrorCode.InvalidParameters);

			if (UserId != adUnitData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Update to db
			var updateQuery = new DBQuery_AdUnit_Update();
			var item = updateQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			if (await DBThread.Instance.ReqQueryAsync(updateQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdUnitModels.Modify.Response();
			if (await response.FromDBAsync(item.AdUnitId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 유닛 삭제
		[HttpPost]
		public async Task<IActionResult> Delete([FromBody] AdUnitModels.Delete.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey adUnitId = request.AdUnitId;
			AdUnitData adUnitData = new AdUnitData();

			// Check validation
			if (await adUnitData.FromDBAsync(adUnitId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != adUnitData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Delete from db
			var deleteQuery = new DBQuery_AdUnit_Delete();
			deleteQuery.IN.DBKey = request.AdUnitId;
			if (await DBThread.Instance.ReqQueryAsync(deleteQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			return Success();
		}

		// -------------------------------------------------------------------------------
		// 광고 유닛 목록
		[HttpPost]
		public async Task<IActionResult> List([FromBody] AdUnitModels.List.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			AdUnitDataContainer container = new AdUnitDataContainer();
			await container.FromDBByAppIdAsync(request.AppId);

			// Response
			var response = new AdUnitModels.List.Response();
			response.AdUnits = container.ToArray();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 

		#endregion // Internal Methods

	}
}