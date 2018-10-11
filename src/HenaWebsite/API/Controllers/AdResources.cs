using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Library.Extensions;
using Hena.Security.Claims;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.API.AdResource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json", "multipart/form-data")]
	[Route("api/[controller]/[action]")]
	[Authorize]
	public class AdResources : BaseApi
	{
		public const int MAX_RESOURCE_SIZE = 1024 * 150;
		public static readonly string[] SUPPORT_IMAGE_CONTENT_TYPE = new string[]{ "image/jpg"
			,"image/png"
			,"image/gif"
		};

		#region API
		// -------------------------------------------------------------------------------
		// 광고 리소스 업로드
		[HttpPost]
		public async Task<IActionResult> Upload([FromForm]AdResourceModels.Upload.Request request)
		{
			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Check validation
			byte[] contents;
			SKBitmap bitmap = null;
			try
			{
				if (SUPPORT_IMAGE_CONTENT_TYPE.Contains(request.File.ContentType) == false)
					return APIResponse(ErrorCode.NotSupportFormat);

				var stream = request.File.OpenReadStream();
				if (stream.Length > MAX_RESOURCE_SIZE)
					return APIResponse(ErrorCode.InvalidResource);

				contents = new byte[stream.Length];
				await stream.ReadAsync(contents, 0, contents.Length);

				bitmap = SKBitmap.Decode(contents);
				if (bitmap.Width >= short.MaxValue || bitmap.Height >= short.MaxValue)
					return APIResponse(ErrorCode.InvalidResource);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
				return APIResponse(ErrorCode.UknownError);
			}

			// Generate Resource Id
			var adResourceId = IDGenerator.NewAdResourceId;

			var remotePath = $"aic/resources/{UserId}/{adResourceId}";

			// Upload to ftp

			FtpClient ftp = new FtpClient("ftp://hena.io", "henacoin", "henacoin2");
			if (await ftp.UploadAsync(remotePath, contents) == false)
			{
				await ftp.CreateDirectoryAsync($"aic");
				await ftp.CreateDirectoryAsync($"aic/resources");
				await ftp.CreateDirectoryAsync($"aic/resources/{UserId}");

				if (await ftp.UploadAsync(remotePath, contents) == false)
				{
					return APIResponse(ErrorCode.Failed);
				}
			}

			// Insert to db
			var insertQuery = new DBQuery_AdResource_Insert();
			var item = insertQuery.IN.Item;
			item.UserId = UserId;
			item.AdResourceId = adResourceId;
			item.AdResourceType = AdResourceTypes.Image;
			item.ContentType = request.File.ContentType;
			item.Width = (short)bitmap.Width;
			item.Height = (short)bitmap.Height;

			if (await DBThread.Instance.ReqQueryAsync(insertQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			var response = new AdResourceModels.Upload.Response();
			if (await response.FromDBAsync(item.AdResourceId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 리소스 삭제
		[HttpPost]
		public async Task<IActionResult> Delete([FromBody] AdResourceModels.Delete.Request request)
		{
			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			DBKey adResourceId = request.AdResourceId;
			AdResourceData adResourceData = new AdResourceData();

			// Check validation
			if (await adResourceData.FromDBAsync(adResourceId) == false)
				return APIResponse(ErrorCode.DatabaseError);

			if (UserId != adResourceData.UserId)
				return APIResponse(ErrorCode.BadRequest);

			// Delete from db
			var deleteQuery = new DBQuery_AdResource_Delete();
			deleteQuery.IN.DBKey = request.AdResourceId;
			if (await DBThread.Instance.ReqQueryAsync(deleteQuery) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// Response
			return Success();
		}

		// -------------------------------------------------------------------------------
		// 광고 리소스 정보
		[HttpPost]
		public async Task<IActionResult> Info([FromBody] AdResourceModels.Info.Request request)
		{
			// Check valid parameters
			if (request == null || request.IsValidParameters() == false)
				return APIResponse(ErrorCode.InvalidParameters);

			// Response
			var response = new AdResourceModels.Info.Response();
			if (await response.FromDBAsync(request.AdResourceId) == false)
				return APIResponse(ErrorCode.BadRequest);

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 광고 리소스 목록
		[HttpPost]
		public async Task<IActionResult> List()
		{
			// Check valid parameters
			AdResourceDataContainer container = new AdResourceDataContainer();
			await container.FromDBByUserIdAsync(UserId);

			// Response
			var response = new AdResourceModels.List.Response();
			response.AdResources = container.ToArray();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 

		#endregion // Internal Methods

	}
}