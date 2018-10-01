using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Produces("application/json")]
	//[Route("api/{language:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/User/[action]")]
	[Route("api/User/[action]")]
	public class UserApiController : BaseController
	{
		// 회원가입 가능한 이메일인지 체크
		[HttpPost]
		public async Task<IActionResult> JoinVerifyEMail([FromBody] RequestUserJoinVerifyEMailModel model)
		{
			// check format
			if (model.EMail.IsValidEmailAddress() == false)
				return Responsed(ErrorCode.InvalidEMail);

			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(model.EMail))
				return Responsed(ErrorCode.ExistEMail);

			return Success();
		}

		// 회원가입 가능한 유저네임인지 체크
		[HttpPost]
		public async Task<IActionResult> JoinVerifyUsername([FromBody] RequestUserJoinVerifyUsernameModel model)
		{
			// check format
			if (model.Username.IsValidUsername() == false)
				return Responsed(ErrorCode.InvalidUserName);

			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByUserNameAsync(model.Username))
				return Responsed(ErrorCode.ExistUserName);

			return Success();
		}

		// 회원가입 요청
		[HttpPost]
		public async Task<IActionResult> Join([FromBody] RequestUserJoinModel model)
		{
			// check format
			if (model.EMail.IsValidEmailAddress() == false)
				return Responsed(ErrorCode.InvalidEMail);

			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(model.EMail))
				return Responsed(ErrorCode.ExistEMail);

			// check verify code
			ErrorCode emailVerifyResult = CheckEMailVerifyCode(model.EMail, model.VerifyCode, false);
			if( emailVerifyResult != ErrorCode.Success )
			{
				return Responsed(emailVerifyResult);
			}

			// insert database
			DBQuery_User_Insert query = new DBQuery_User_Insert();
			basicData = query.IN.BasicData;
			basicData.UserDBKey = IDGenerator.NewUserId;
			basicData.EMail = model.EMail;
			basicData.CreateTime = DateTime.UtcNow;
			basicData.Password = PasswordUtility.HashPassword(model.Password);

			if (await DBThread.Instance.ReqQueryAsync(query) == false)
				return Responsed(ErrorCode.DatabaseError);

			// response
			ResponseUserJoinModel responseData = new ResponseUserJoinModel();
			responseData.EMail = model.EMail;
			responseData.CreateTime = basicData.CreateTime;

			return Success(responseData);
		}

		// 이메일 인증코드 검증
		private ErrorCode CheckEMailVerifyCode(string email, string verifyCode, bool checkOnly)
		{
			// check verify code
			var verifyData = VerifyDataManager.Instance.Find(verifyCode);
			if (verifyData == null)
				return ErrorCode.InvalidVerifyCode;

			var verifyEMail = verifyData.UserData as RequestUserSendVerifyEMailModel;
			if (verifyEMail == null)
				return ErrorCode.InvalidVerifyCode;

			if (email != verifyEMail.EMail)
				return ErrorCode.InvalidVerifyCode;

			if( checkOnly == false )
			{
				if (verifyData.TrySetVerified() == false)
					return ErrorCode.InvalidVerifyCode;
			}

			return ErrorCode.Success;
		}

		// 이메일 인증코드 체크
		[HttpPost]
		public IActionResult IsValidEMailVerifyCode([FromBody] RequestUserIsValidEMailVerifyCode model)
		{
			return Responsed(CheckEMailVerifyCode(model.EMail, model.VerifyCode, true));
		}

		// 이메일 인증 요청
		[HttpPost]
		public async Task<IActionResult> SendVerifyEMail([FromBody] RequestUserSendVerifyEMailModel model)
		{
			// check account
			UserBasicData basicData = new UserBasicData();
			if( model.EMail.IsValidEmailAddress() == false)
				return Responsed(ErrorCode.InvalidEMail);

			if (await basicData.FromDBByEmailAsync(model.EMail))
				return Responsed(ErrorCode.ExistEMail);

			// generate verify data
			var verifyData = VerifyDataManager.Instance.NewVerifyData(TimeSpan.FromMinutes(3)
				, TimeSpan.FromHours(1), true, model);

			// 이메일 발송
			StringBuilder msg = new StringBuilder(1024);
			msg.AppendLine(string.Format($"Hello. "));
			msg.AppendLine(string.Format($"Please enter your verification code below and complete verification."));
			msg.AppendLine();
			msg.AppendLine(string.Format($"CODE : {verifyData.VerifyCode}"));

			await WebServiceUtility.SendEMailAsync("[Hena Platform] Signup verification code.", msg.ToString(), false, model.EMail);

			// 메일 발송
			ResponseUserSendVerifyEMailModel responseData = new ResponseUserSendVerifyEMailModel();
			responseData.EMail = model.EMail;

			return Success(responseData);
		}

		// 비밀번호 리셋
		[HttpPost]
		public async Task<IActionResult> ResetPassword([FromBody] RequestUserResetPasswordModel model)
		{
			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(model.EMail) == false)
				return Responsed(ErrorCode.InvalidEMail);

			string newPassword = IDGenerator.NewVerifyCode;
			basicData.Password = PasswordUtility.HashPassword(newPassword);

			var query = new DBQuery_User_Update_Password();
			query.IN.UserDBKey = basicData.UserDBKey;
			query.IN.Password = basicData.Password;

			if (await DBThread.Instance.ReqQueryAsync(query) == false)
				return Responsed(ErrorCode.DatabaseError);


			// 이메일 발송
			StringBuilder msg = new StringBuilder(1024);
			msg.AppendLine(string.Format($"Hello. {basicData.EMail.Split('@')[0]}"));
			msg.AppendLine(string.Format($"Reseted your password."));
			msg.AppendLine();
			msg.AppendLine(string.Format($"Your temp password : {newPassword}"));

			await WebServiceUtility.SendEMailAsync("[Hena Platform] Reseted your password.", msg.ToString(), false, model.EMail);


			return Success();

		}
	}
}