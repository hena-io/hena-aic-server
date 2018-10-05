using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.Users
{
	public static partial class APIModels
	{
		// 회원가입 가능한 이메일인지 체크
		public static class JoinVerifyEMail
		{
			public class Request
			{
				public string EMail { get; set; } = string.Empty;
			}

			public class Response
			{
			}
		}
		
		// 비밀번호 초기화
		public static class ResetPassword
		{
			public class Request
			{
				public string EMail { get; set; } = string.Empty;
			}

			public class Response { }
		}

		// 회원가입
		public static class Join
		{
			public class Request
			{
				public string EMail { get; set; }
				public string Password { get; set; }
				public string VerifyCode { get; set; }
			}

			public class Response
			{
				public string EMail { get; set; }
				public DateTime CreateTime { get; set; }
			}
		}

		// 이메일 인증코드 체크
		public static class IsValidEMailVerifyCode
		{
			public class Request
			{
				public string EMail { get; set; }
				public string VerifyCode { get; set; }
			}

			public class Response
			{
			}
		}

		// 이메일 인증 요청
		public static class SendVerifyEMail
		{
			public class Request
			{
				public string EMail { get; set; }
			}

			public class Response
			{
				public string EMail { get; set; }
				public DateTime SendTime { get; set; }
			}
		}
	}
}
