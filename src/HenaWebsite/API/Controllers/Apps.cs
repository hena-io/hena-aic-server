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
using HenaWebsite.Models.API.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class Apps : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 앱 생성
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AppModels.Create.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check validation

			// Insert to db
			var insertQuery = new DBQuery_App_Insert();
			var item = insertQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			item.AppId = IDGenerator.NewAppId;
			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AppModels.Create.Response();
			if (await response.FromDBAsync(item.AppId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 앱 수정
		[HttpPost]
		public async Task<IActionResult> Modify([FromBody] AppModels.Modify.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey appId = request.AppId;
			AppData appData = new AppData();

			// Check validation
			if (await appData.FromDBAsync(appId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != appData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Update to db
			var updateQuery = new DBQuery_App_Update();
			var item = updateQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			if (await DBThread.Instance.ReqQueryAsync(updateQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AppModels.Modify.Response();
			if (await response.FromDBAsync(item.AppId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 앱 삭제
		[HttpPost]
		public async Task<IActionResult> Delete([FromBody] AppModels.Delete.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey appId = request.AppId;
			AppData appData = new AppData();

			// Check validation
			if (await appData.FromDBAsync(appId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != appData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Delete from db
			var deleteQuery = new DBQuery_App_Delete();
			deleteQuery.IN.DBKey = request.AppId;
			if (await DBThread.Instance.ReqQueryAsync(deleteQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			return Success();
		}

		// -------------------------------------------------------------------------------
		// 앱 목록
		[HttpPost]
		public async Task<IActionResult> List()
		{
			AppDataContainer container = new AppDataContainer();
			await container.FromDBByUserIdAsync(UserId);

			// Response
			var response = new AppModels.List.Response();
			response.Apps = container.ToArray();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 

		#endregion // Internal Methods

	}
}