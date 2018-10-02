using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.Campaigns
{
	#region API
	public class APIReqeust_CreateCampaign
	{
		public string CampaignName { get; set; } = string.Empty;
		public DateTime BeginTime { get; set; } = DateTime.MinValue;
		public DateTime EndTime { get; set; } = DateTime.MinValue;
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
		public string EMail { get; set; }
		public string Password { get; set; }
		public string VerifyCode { get; set; }
	}

	public class ResponseUserJoinModel : IResponseData
	{
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
		public string EMail { get; set; }
	}

	public class ResponseUserSendVerifyEMailModel : IResponseData
	{
		public string EMail { get; set; }
		public DateTime SendTime { get; set; }
	}
	#endregion // Verify EMail
	#endregion // API
}
