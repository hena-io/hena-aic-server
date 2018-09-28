using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Hena.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
    public static class WebServiceUtility
    {
		
		public static RedirectResult RedirectToActionWithNonce(this Controller controller, string actionName, string controllerName)
		{
			return controller.Redirect(controller.Url.Action(actionName, controllerName) + $"?t={DateTime.UtcNow.Ticks}");
		}
        public static TimeSpan GetTimeZoneOffsetFromUTC(this Controller controller)
        {
            return GetTimeZoneOffsetFromUTC(controller.User);
        }

        public static TimeSpan GetTimeZoneOffsetFromUTC(ClaimsPrincipal claims)
        {
            var timeZoneOffsetString = claims.FindFirstValue("TimeZoneOffset");
            if (string.IsNullOrEmpty(timeZoneOffsetString))
                return TimeSpan.Zero;

            TimeSpan timeZoneOffset;
            if (TimeSpan.TryParse(timeZoneOffsetString, out timeZoneOffset) == false)
                return TimeSpan.Zero;

            return timeZoneOffset;
        }

		public static async Task SendEMailAsync(string subject, string body, bool isBodyHtml, string to)
		{
			await SendEMailAsync(subject, body, isBodyHtml, new MailAddress[] { new MailAddress(to) }, null, null);
		}
		public static async Task SendEMailAsync(string subject, string body, bool isBodyHtml, MailAddress to)
		{
			await SendEMailAsync(subject, body, isBodyHtml, new MailAddress[] { to }, null, null);
		}
		public static async Task SendEMailAsync(string subject, string body, bool isBodyHtml, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc)
		{
			WebConfiguration config = WebConfiguration.Instance;

			// 활성화 메일 전송
			SmtpClient client = new SmtpClient(config.SupportEmailSmtpHost, config.SupportEmailSmtpPort);
			client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
			client.EnableSsl = true;  // SSL을 사용한다.
			client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
			client.Credentials = new System.Net.NetworkCredential(config.SupportEmailId, config.SupportEmailPassword);

			MailAddress from = new MailAddress(config.SupportEmailId, config.SupportEmailDisplayName, Encoding.UTF8);

			MailMessage mailMessage = new MailMessage();
			mailMessage.From = from;
			mailMessage.To.AddRangeSafe(to);
			//if (mailMessage.To.Count == 0)
			//{
			//	mailMessage.To.Add(new MailAddress(from.Address));
			//}
			mailMessage.CC.AddRangeSafe(cc);
			mailMessage.Bcc.AddRangeSafe(bcc);
			mailMessage.SubjectEncoding = Encoding.UTF8;
			mailMessage.BodyEncoding = Encoding.UTF8;
			mailMessage.Subject = subject;
			mailMessage.Body = body;
			mailMessage.IsBodyHtml = isBodyHtml;

			try
			{
				// 메일 전송
				await client.SendMailThreadAsync(mailMessage);

				// Clean up.
				mailMessage.Dispose();
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
			}
		}

		public static SmtpClient CreateSMTPClient_GMail(string mail, string password)
		{
			SmtpClient client = new SmtpClient("smtp-relay.gmail.com", 587);
			client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
			client.EnableSsl = true;  // SSL을 사용한다.
			client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
			client.Credentials = new System.Net.NetworkCredential(mail, password);
			return client;
		}

		public static string QueryAsString(this HttpContext context, string key, string defaultValue = "")
        {
            return QueryAsString(context.Request, key, defaultValue);
        }
        public static string QueryAsString(this HttpRequest request, string key, string defaultValue = "")
        {
            StringValues value;
            if (request.Query.TryGetValue(key, out value))
            {
                return value;
            }
            return defaultValue;
        }

		// 강제로 string 배열로 변환한다.
		// string 배열 타입이 아닐경우 비어있는 배열을 반환한다.
		public static string[] ToStringArrayForce(object target)
		{
			if (target is string[])
				return target as string[];

			if (target is string)
				return new string[1] { target as string };

			return new string[0];
		}

		// 
		public static string ToStringForce(object target)
		{
			if (target is string)
				return target as string;

			return string.Empty;
		}

		
	}
}
