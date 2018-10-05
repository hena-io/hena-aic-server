﻿using System;
using Microsoft.AspNetCore.Mvc;
using Hena;
using Hena.Security.Claims;

namespace HenaWebsite.Controllers
{
    public class BaseController : Controller
    {
		#region Properties
		protected bool IsAuthenticated { get => User.Identity.IsAuthenticated; }
		protected DBKey UserDBKey { get => GetClaimValueSafe(HenaClaimTypes.SerialNumber, GlobalDefine.INVALID_DBKEY); }
		protected string EMail { get => GetClaimValueSafe(HenaClaimTypes.Email, string.Empty); }
		#endregion // Properties

		#region Utility
		protected virtual string GetClaimValueSafe(string type, string defaultValue = "")
		{
			try
			{
				var claim = User.FindFirst(type);
				if (claim == null)
				{
					return defaultValue;
				}
				return claim.Value;
			}
			catch (Exception) { }
			return defaultValue;
		}
		#endregion // Utility

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