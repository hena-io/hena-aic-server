using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hena;
using Hena.Security.Claims;
using HenaWebsite.Models;
using HenaWebsite.Models.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace HenaWebsite.Controllers.API
{
	public class BaseApi : BaseController
	{
		#region Classes
		protected class __APIResponse
		{
			// 결과
			public ErrorCode Result { get; set; }

			// 메시지
			public string Message { get; set; } = string.Empty;

			// 데이터
			public object Data { get; set; }
		}
		#endregion	// Classes

		#region Base API
		protected virtual IActionResult Success(object data = null)
		{
			return APIResponse(ErrorCode.Success, string.Empty, data);
		}

		protected virtual IActionResult Failed(string message = "", object data = null)
		{
			return APIResponse(ErrorCode.Failed, string.Empty, data);
		}

		protected virtual IActionResult APIResponse(ErrorCode errorCode, string message = "", object data = null)
		{
			return APIResponse(new __APIResponse() { Result = errorCode, Message = message, Data = data });
		}

		protected virtual IActionResult APIResponse(__APIResponse responseData)
		{
			return Ok(responseData);
		}
		#endregion // Base API
	}
}