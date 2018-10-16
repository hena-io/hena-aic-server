using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.Service.PageAd;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Produces("application/json")]
	[Route("service/[controller]/[action]")]
	public class PageAdController : API.BaseApi
	{
		// 광고 준비
		public async Task<IActionResult> AdReaady([FromBody]PageAdModels.AdReady.Request request)
		{
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check Data
			AdInfo ai = new AdInfo();

			if (await ai.AdUnitData.FromDBAsync(request.AdUnitId) == false 
				|| await ai.AppData.FromDBAsync(ai.AdUnitData.AppId) == false)
			{
				return APIResponse(ErrorCode.BadRequest);
			}

			if (ai.AdUnitData.AdSystemType != request.AdSystemType)
				return APIResponse(ErrorCode.InvalidParameters);


			var supportedDesignTypes = AdDesignTypes.ToSupported(request.ClientType, request.AdSystemType, request.IsLandscape);

			bool isFound = false;
			for( int i = 0; i < 3; ++i)
			{
				var adDesignType = supportedDesignTypes[RandomEx.Range(0, supportedDesignTypes.Length)];
				if (await ai.AdDesignData.ChoiceFromDBAsync(adDesignType) 
					&& await ai.CampaignData.FromDBAsync(ai.AdDesignData.CampaignId)
					&& await ai.AdResourceData.FromDBAsync(ai.AdDesignData.AdResourceId)
					)
				{
					isFound = true;
					break;
				}
			}

			if (isFound == false)
				return APIResponse(ErrorCode.NotFound);

			// Insert to db
			var insertQuery = new DBQuery_AdHistory_Insert();
			var ahd = ai.AdHistoryData = insertQuery.IN.Item;
			ahd.AdHistoryId = IDGenerator.NewAdHistoryId;

			ahd.PublisherId = ai.AdUnitData.UserId;
			ahd.AppId = ai.AdUnitData.AppId;
			ahd.AdUnitId = ai.AdUnitData.AdUnitId;

			ahd.AdvertiserId = ai.CampaignData.UserId;
			ahd.AdDesignId = ai.AdDesignData.AdDesignId;
			ahd.CampaignId = ai.CampaignData.CampaignId;

			ahd.UserAgent = UserAgent;
			ahd.IPAddress = RemoteIPAddress;

			ahd.AdDesignType = ai.AdDesignData.AdDesignType;
			ahd.CampaignType = ai.CampaignData.CampaignType;
			ahd.Cost = ai.CampaignData.Cost;

			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new PageAdModels.AdReady.Response();
			response.AdSystemType = ai.AdUnitData.AdSystemType;
			response.AdDesignType = ai.AdDesignData.AdDesignType;

			response.ContentType = ai.AdResourceData.ContentType;
			response.ResourceUrl = ai.AdResourceData.Url;
			response.Width = ai.AdResourceData.Width;
			response.Height = ai.AdResourceData.Height;

			response.AdUrl = ai.AdDesignData.DestinationUrl;
			response.AdClickUrl = Url.Action("AdClick", "PageAd", HttpContext.Request.Host);

			response.Ai = ai.Encode();
			return Success(response);
		}

		// 광고 시청
		public async Task<IActionResult> AdDisplay([FromBody]PageAdModels.AdDisplay.Request request)
		{
			try
			{
				if (request == null || request.IsValidParameters() == false)
					return APIResponse(ErrorCode.InvalidParameters);

				AdInfo ai = new AdInfo(request.Ai);

				if (request.AdUnitId != ai.AdUnitData.AdUnitId)
					return APIResponse(ErrorCode.BadRequest);

				var query = new DBQuery_AdHistory_Update_Display();
				query.IN.AdHistoryId = ai.AdHistoryData.AdHistoryId;
				query.IN.IsDisplayed = true;

				if (await DBThread.Instance.ReqQueryAsync(query) == false)
					return APIResponse(ErrorCode.DatabaseError);

				return Success();
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return APIResponse(ErrorCode.UknownError);
			}
		}

		// 광고 클릭
		public async Task<IActionResult> AdClick([FromBody]PageAdModels.AdClick.Request request)
		{
			try
			{
				if (request == null || request.IsValidParameters() == false)
					return APIResponse(ErrorCode.InvalidParameters);

				AdInfo ai = new AdInfo(request.Ai);

				if (request.AdUnitId != ai.AdUnitData.AdUnitId)
					return APIResponse(ErrorCode.BadRequest);

				var query = new DBQuery_AdHistory_Update_Click();
				query.IN.AdHistoryId = ai.AdHistoryData.AdHistoryId;
				query.IN.IsClicked = true;

				if (await DBThread.Instance.ReqQueryAsync(query) == false)
					return APIResponse(ErrorCode.DatabaseError);

				return Redirect(ai.AdDesignData.DestinationUrl);
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return View("");
			}
		}
	}
}