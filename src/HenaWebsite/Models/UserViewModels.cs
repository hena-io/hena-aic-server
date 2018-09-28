using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.User
{
	// Login
	public class LoginFormModel
	{
		public string EmailOrUserName { get; set; }
		public string Password { get; set; }

		public int TimeZoneOffsetFromUTC { get; set; } = 0;
	}

	// Sign up
	public class SignUpFromModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}

	#region API

	public class RequestUserJoinVerifyEMailModel
	{
		public string EMail { get; set; } = string.Empty;
	}

	public class RequestUserJoinVerifyUsernameModel
	{
		public string Username { get; set; } = string.Empty;
	}

	#region Reset Password
	public class RequestUserResetPasswordModel
	{
		public string EMail { get; set; }
	}
	#endregion // Verify Code



	#region User Join
	public class RequestUserJoinModel
	{
		public string Username { get; set; }
		public string EMail { get; set; }
		public string Password { get; set; }
		public string VerifyCode { get; set; }
	}

	public class ResponseUserJoinModel : IResponseData
	{
		public string Username { get; set; }
		public string EMail { get; set; }
		public DateTime CreateTime { get; set; }
	}
	#endregion // User Join

	#region Verify Code
	public class RequestUserIsValidEMailVerifyCode
	{
		public string EMail { get; set; }
		public string VerifyCode { get; set; }
	}
	#endregion // Verify Code

	#region Verify EMail
	public class RequestUserSendVerifyEMailModel
	{
		public string Username { get; set; }
		public string EMail { get; set; }
	}

	public class ResponseUserSendVerifyEMailModel : IResponseData
	{
		public string Username { get; set; }
		public string EMail { get; set; }
		public DateTime SendTime { get; set; }
	}
	#endregion // Verify EMail
	#endregion // API
}
