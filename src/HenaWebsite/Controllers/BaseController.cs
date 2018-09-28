using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hena;
using Hena.DB;
using Hena.Shared;
using Hena.Shared.Data;
using Newtonsoft.Json.Linq;
using HenaWebsite.Models;

namespace HenaWebsite.Controllers
{
    public class BaseController : Controller
    {
		protected virtual IActionResult Success(IResponseData data = null)
		{
			return ResponseByData(ErrorCode.Success, string.Empty, data);
		}

        protected virtual IActionResult Success(JToken data)
        {
			return ResponseByJson(ErrorCode.Success, string.Empty, data);
		}

        protected virtual IActionResult Failed(string message = "")
		{
			return ResponseByData(ErrorCode.Failed, message, null);
		}

		protected virtual IActionResult ResponseByData(ErrorCode errorCode, string message, IResponseData data)
		{
			return Ok(new DataResponse() { Result = errorCode, Message = message, Data = data });
		}

		protected virtual IActionResult ResponseByJson(ErrorCode errorCode, string message, JToken data)
		{
			return Ok(new JSONResponse() { Result = errorCode, Message = message, Data = data });
		}

		protected virtual IActionResult Responsed(ErrorCode errorCode, string message = "")
		{
			return Ok(new DataResponse() { Result = errorCode, Message = message });
		}

		protected virtual IActionResult Responsed(ResponseBase response)
		{
			return Ok(response);
		}

		//protected async Task SendEMailAsync(string subject, string body, bool isBodyHtml, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc)
		//{
		//	// 활성화 메일 전송
		//	SmtpClient client = MailUtility.CreateSMTPClient_GMail(EZConfig.Instance.SupportEmailId, EZConfig.Instance.SupportEmailPassword);
		//	MailAddress from = new MailAddress(EZConfig.Instance.SupportEmailId, EZConfig.Instance.SupportEmailDisplayName, Encoding.UTF8);

		//	MailMessage mailMessage = new MailMessage();
		//	mailMessage.To.AddRangeSafe(to);
		//	mailMessage.CC.AddRangeSafe(to);
		//	mailMessage.Bcc.AddRangeSafe(to);
		//	mailMessage.SubjectEncoding = Encoding.UTF8;
		//	mailMessage.BodyEncoding = Encoding.UTF8;
		//	mailMessage.Subject = subject;
		//	mailMessage.Body = body;
		//	mailMessage.IsBodyHtml = isBodyHtml;

		//	try
		//	{
		//		// 메일 전송
		//		await client.SendMailThreadAsync(mailMessage);

		//		// Clean up.
		//		mailMessage.Dispose();
		//	}
		//	catch (Exception ex)
		//	{
		//		Logger.Global.LogException(ex);
		//	}
		//}
	}
}