using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Security.Claims;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.API.AdDesign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class AdDesigns : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 광고 디자인 생성
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AdDesignModels.Create.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check validation

			// Insert to db
			var insertQuery = new DBQuery_AdDesign_Insert();
			var item = insertQuery.IN.Item;
			item.UserId = UserId;
			item.AdDesignId = IDGenerator.NewAdDesignId;
			item.CreateTime = DateTime.UtcNow;
			request.Fill(insertQuery.IN.Item);
			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdDesignModels.Create.Response();
			if (await response.FromDBAsync(item.AdDesignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 디자인 수정
		[HttpPost]
		public async Task<IActionResult> Modify([FromBody] AdDesignModels.Modify.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey adDesignId = request.Id.ToLong();
			AdDesignData adDesignData = new AdDesignData();

			// Check validation
			if (await adDesignData.FromDBAsync(adDesignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != adDesignData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Update to db
			var updateQuery = new DBQuery_AdDesign_Update();
			var item = updateQuery.IN.Item;
			item.UserId = UserId;
			item.AdDesignId = request.Id.ToLong();
			request.Fill(updateQuery.IN.Item);
			if (await DBThread.Instance.ReqQueryAsync(updateQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdDesignModels.Modify.Response();
			if (await response.FromDBAsync(item.AdDesignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 디자인 삭제
		[HttpPost]
		public async Task<IActionResult> Delete([FromBody] AdDesignModels.Delete.Request request)
		{
			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey adDesignId = request.Id.ToLong();
			AdDesignData adDesignData = new AdDesignData();

			// Check validation
			if (await adDesignData.FromDBAsync(adDesignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != adDesignData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Delete from db
			var deleteQuery = new DBQuery_AdDesign_Delete();
			deleteQuery.IN.DBKey = request.Id.ToLong();
			if (await DBThread.Instance.ReqQueryAsync(deleteQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdDesignModels.Delete.Response();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 

		#endregion // Internal Methods

	}
}