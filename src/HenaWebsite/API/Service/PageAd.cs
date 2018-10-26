using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
	public class PageAd : API.BaseApi
	{
		// 광고 준비
		[HttpPost]
		public async Task<IActionResult> AdReady([FromBody]PageAdModels.AdReady.Request request)
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

			UserBasicData customerData = new UserBasicData();
			if (request.CustomerId != GlobalDefine.INVALID_DBKEY 
				&& await customerData.FromDBAsync(request.CustomerId))
			{
				ahd.CustomerId = request.CustomerId;
			}
			else
			{
				ahd.CustomerId = GlobalDefine.INVALID_DBKEY;
			}

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
			response.AdUnitId = request.AdUnitId;
			response.AdSystemType = ai.AdUnitData.AdSystemType;
			response.AdDesignType = ai.AdDesignData.AdDesignType;

			response.ContentType = ai.AdResourceData.ContentType;
			var resourceUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Url.Action("AdResource", "PageAd")}";
			response.ResourceUrl = resourceUrl;

			response.Width = ai.AdResourceData.Width;
			response.Height = ai.AdResourceData.Height;

			response.AdUrl = ai.AdDesignData.DestinationUrl;

			response.AdClickUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{Url.Action("AdClick", "PageAd")}";

			response.Ai = ai.Encode().EncodeBase64ToUrlSafeBase64();
			return Success(response);
		}

		// 리소스 요청( Display 상태로 판단 )
		[HttpGet]
		public async Task<IActionResult> AdResource([FromQuery, Required]PageAdModels.AdResource.Request request)
		{
			try
			{
				if (request == null || request.IsValidParameters() == false)
					return NotFound();

				AdInfo ai = request.CreateFromAi();

				AdHistoryData ahd = new AdHistoryData();
				if (await ahd.FromDBAsync(ai.AdHistoryData.AdHistoryId) == false)
					return NotFound();

				if (ahd.IsDisplayed)
					return Redirect(ai.AdResourceData.Url);

				var updateDisplayQuery = new DBQuery_AdHistory_Update_Display();
				updateDisplayQuery.IN.AdHistoryId = ai.AdHistoryData.AdHistoryId;
				updateDisplayQuery.IN.IsDisplayed = true;

				if (await DBThread.Instance.ReqQueryAsync(updateDisplayQuery))
				{
					// 수익 지급
					if (ai.AdHistoryData.CampaignType == CampaignTypes.CPM)
					{
						// 각 뷰당 1/1000로 계산
						var cost = ahd.Cost * 0.001m;
						await ProvideRevenueToDBAsync(ahd.AdHistoryId, ahd.PublisherId, ahd.CustomerId, cost * 0.6m, cost * 0.4m);
						await PaymentCostToDBAsync(ahd.AdvertiserId, cost);
					}
				}

				return Redirect(ai.AdResourceData.Url);
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return NotFound();
			}
		}


		// 광고 클릭
		[HttpGet]
		public async Task<IActionResult> AdClick([FromQuery]PageAdModels.AdClick.Request request)
		{
			try
			{
				if (request == null || request.IsValidParameters() == false)
					return APIResponse(ErrorCode.InvalidParameters);

				AdInfo ai = request.CreateFromAi();

				AdHistoryData ahd = new AdHistoryData();
				if (await ahd.FromDBAsync(ai.AdHistoryData.AdHistoryId) == false)
					return Redirect(ai.AdDesignData.GetDestinationUrl());

				if (ahd.IsClicked)
					return Redirect(ai.AdDesignData.GetDestinationUrl());

				var updateClickQuery = new DBQuery_AdHistory_Update_Click();
				updateClickQuery.IN.AdHistoryId = ai.AdHistoryData.AdHistoryId;
				updateClickQuery.IN.IsClicked = true;
				if (await DBThread.Instance.ReqQueryAsync(updateClickQuery))
				{
					// 수익 지급
					if (ai.AdHistoryData.CampaignType == CampaignTypes.CPC)
					{
						await ProvideRevenueToDBAsync(ahd.AdHistoryId, ahd.PublisherId, ahd.CustomerId, ahd.Cost * 0.6m, ahd.Cost * 0.4m);
						await PaymentCostToDBAsync(ahd.AdvertiserId, ahd.Cost);
					}
				}


				return Redirect(ai.AdDesignData.GetDestinationUrl());
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return View("");
			}
		}

		// 비용 차감
		private async Task PaymentCostToDBAsync(DBKey userId, decimal cost)
		{
			if (userId > 0 && cost > 0)
			{
				var addBalanceQuery = new DBQuery_Balance_Add();
				addBalanceQuery.IN.UserId = userId;
				addBalanceQuery.IN.CurrencyType = CurrencyTypes.HENA;
				addBalanceQuery.IN.Amount = -cost;

				await DBThread.Instance.ReqQueryAsync(addBalanceQuery);
			}
		}

		// 수익금 지급
		private async Task ProvideRevenueToDBAsync(DBKey adHistoryId, DBKey publisherId, DBKey customerId, decimal publisherRevenue, decimal customerRevenue)
		{
			decimal realPublisherRevenue = publisherRevenue;
			decimal realCustomerRevenue = customerRevenue;
			if (customerId == GlobalDefine.INVALID_DBKEY)
			{
				realPublisherRevenue += realCustomerRevenue;
				realCustomerRevenue = 0m;
			}

			var updateRevenueQuery = new DBQuery_AdHistory_Update_Revenue();
			updateRevenueQuery.IN.AdHistoryId = adHistoryId;
			updateRevenueQuery.IN.PublisherRevenue = realPublisherRevenue;
			updateRevenueQuery.IN.CustomerRevenue = realCustomerRevenue;
			await DBThread.Instance.ReqQueryAsync(updateRevenueQuery);

			if (publisherId > 0 && realPublisherRevenue > 0)
			{
				var addBalanceQuery = new DBQuery_Balance_Add();
				addBalanceQuery.IN.UserId = publisherId;
				addBalanceQuery.IN.CurrencyType = CurrencyTypes.HENA_AIC;
				addBalanceQuery.IN.Amount = realPublisherRevenue;

				await DBThread.Instance.ReqQueryAsync(addBalanceQuery);
			}

			if (customerId > 0 && realCustomerRevenue > 0)
			{
				var addBalanceQuery = new DBQuery_Balance_Add();
				addBalanceQuery.IN.UserId = customerId;
				addBalanceQuery.IN.CurrencyType = CurrencyTypes.HENA_AIC;
				addBalanceQuery.IN.Amount = realCustomerRevenue;

				await DBThread.Instance.ReqQueryAsync(addBalanceQuery);
			}
		}
	}
}