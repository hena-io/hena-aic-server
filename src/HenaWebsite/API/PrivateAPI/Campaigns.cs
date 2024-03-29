﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Library.Extensions;
using Hena.Security.Claims;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.API.Campaign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class Campaigns : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 캠페인 생성
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CampaignModels.Create.Request request)
		{
			// Check session validation
			if( await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check validation

			// Insert to db
			var insertQuery = new DBQuery_Campaign_Insert();
			var item = insertQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			item.CampaignId = IDGenerator.NewCampaignId;
			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new CampaignModels.Create.Response();
			if (await response.FromDBAsync(item.CampaignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 캠페인 수정
		[HttpPost]
		public async Task<IActionResult> Modify([FromBody] CampaignModels.Modify.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey campaignId = request.CampaignId;
			CampaignData campaignData = new CampaignData();

			// Check validation
			if (await campaignData.FromDBAsync(campaignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != campaignData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Update to db
			var updateQuery = new DBQuery_Campaign_Update();
			var item = updateQuery.IN.Item;
			request.Copy(item);
			item.UserId = UserId;
			if (await DBThread.Instance.ReqQueryAsync(updateQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new CampaignModels.Modify.Response();
			if (await response.FromDBAsync(item.CampaignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 캠페인 삭제
		[HttpPost]
		public async Task<IActionResult> Delete([FromBody] CampaignModels.Delete.Request request)
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			// Check valid parameters
			if (request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey campaignId = request.CampaignId;
			CampaignData campaignData = new CampaignData();

			// Check validation
			if (await campaignData.FromDBAsync(campaignId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != campaignData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Delete from db
			var deleteQuery = new DBQuery_Campaign_Delete();
			deleteQuery.IN.DBKey = request.CampaignId;
			if (await DBThread.Instance.ReqQueryAsync(deleteQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			return Success();
		}

		// -------------------------------------------------------------------------------
		// 캠페인 목록
		[HttpPost]
		public async Task<IActionResult> List()
		{
			// Check session validation
			if (await CheckSessionValidationAndSignOutAsync() == false)
				return APIResponse(ErrorCode.InvalidSession);

			CampaignDataContainer container = new CampaignDataContainer();
			await container.FromDBByUserIdAsync(UserId);

			// Response
			var response = new CampaignModels.List.Response();
			response.Campaigns = container.ToArray();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 

		#endregion // Internal Methods

	}
}