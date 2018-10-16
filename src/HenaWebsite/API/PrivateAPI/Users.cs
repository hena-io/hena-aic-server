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
using HenaWebsite.Models.API;
using HenaWebsite.Models.API.User;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers.API
{
	[Produces("application/json")]
	[Route("api/[controller]/[action]")]
	public class Users : BaseApi
	{
		#region API
		// -------------------------------------------------------------------------------
		// 회원가입 가능한 이메일인지 체크
		[HttpPost]
		public async Task<IActionResult> JoinVerifyEMail([FromBody] UserModels.JoinVerifyEMail.Request request)
		{
			// check format
			if (request.EMail.IsValidEmailAddress() == false)
				return APIResponse(ErrorCode.InvalidEMail);

			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(request.EMail))
				return APIResponse(ErrorCode.ExistEMail);

			var response = new UserModels.JoinVerifyEMail.Response();
			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 회원가입 요청
		[HttpPost]
		public async Task<IActionResult> Join([FromBody] UserModels.Join.Request request)
		{
			// check format
			if (request.EMail.IsValidEmailAddress() == false)
				return APIResponse(ErrorCode.InvalidEMail);

			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(request.EMail))
				return APIResponse(ErrorCode.ExistEMail);

			// check verify code
			ErrorCode emailVerifyResult = CheckEMailVerifyCode(request.EMail, request.VerifyCode, false);
			if (emailVerifyResult != ErrorCode.Success)
			{
				return APIResponse(emailVerifyResult);
			}

			// insert database
			DBQuery_User_Insert query = new DBQuery_User_Insert();
			basicData = query.IN.BasicData;
			basicData.UserId = IDGenerator.NewUserId;
			basicData.EMail = request.EMail;
			basicData.CreateTime = DateTime.UtcNow;
			basicData.Password = PasswordUtility.HashPassword(request.Password);

			if (await DBThread.Instance.ReqQueryAsync(query) == false)
				return APIResponse(ErrorCode.DatabaseError);

			// response
			var response = new UserModels.Join.Response();
			response.EMail = request.EMail;
			response.CreateTime = basicData.CreateTime;

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 이메일 인증코드 체크
		[HttpPost]
		public IActionResult IsValidEMailVerifyCode([FromBody] UserModels.IsValidEMailVerifyCode.Request request)
		{
			var errorCode = CheckEMailVerifyCode(request.EMail, request.VerifyCode, true);
			var response = new UserModels.IsValidEMailVerifyCode.Response();
			if (errorCode == ErrorCode.Success)
			{
				return APIResponse(errorCode, string.Empty, response);
			}
			return APIResponse(errorCode);
		}

		// -------------------------------------------------------------------------------
		// 이메일 인증 요청
		[HttpPost]
		public async Task<IActionResult> SendVerifyEMail([FromBody] UserModels.SendVerifyEMail.Request request)
		{
			// check account
			UserBasicData basicData = new UserBasicData();
			if (request.EMail.IsValidEmailAddress() == false)
				return APIResponse(ErrorCode.InvalidEMail);

			if (await basicData.FromDBByEmailAsync(request.EMail))
				return APIResponse(ErrorCode.ExistEMail);

			// generate verify data
			var verifyData = VerifyDataManager.Instance.NewVerifyData(TimeSpan.FromMinutes(3)
				, TimeSpan.FromHours(1), true, request);

			// 이메일 발송
			StringBuilder msg = new StringBuilder(1024);
			msg.AppendLine(string.Format($"Hello. "));
			msg.AppendLine(string.Format($"Please enter your verification code below and complete verification."));
			msg.AppendLine();
			msg.AppendLine(string.Format($"CODE : {verifyData.VerifyCode}"));

			await WebServiceUtility.SendEMailAsync("[Hena Platform] Signup verification code.", msg.ToString(), false, request.EMail);

			// response
			var response = new UserModels.SendVerifyEMail.Response();
			response.EMail = request.EMail;
			response.SendTime = DateTime.UtcNow;

			return Success(response);
		}

		// -------------------------------------------------------------------------------
		// 비밀번호 리셋
		[HttpPost]
		public async Task<IActionResult> ResetPassword([FromBody] UserModels.ResetPassword.Request request)
		{
			// check account
			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(request.EMail) == false)
				return APIResponse(ErrorCode.InvalidEMail);

			string newPassword = IDGenerator.NewVerifyCode;
			basicData.Password = PasswordUtility.HashPassword(newPassword);

			var query = new DBQuery_User_Update_Password();
			query.IN.UserId = basicData.UserId;
			query.IN.Password = basicData.Password;

			if (await DBThread.Instance.ReqQueryAsync(query) == false)
				return APIResponse(ErrorCode.DatabaseError);


			// 이메일 발송
			StringBuilder msg = new StringBuilder(1024);
			msg.AppendLine(string.Format($"Hello. {basicData.EMail.Split('@')[0]}"));
			msg.AppendLine(string.Format($"Reseted your password."));
			msg.AppendLine();
			msg.AppendLine(string.Format($"Your temp password : {newPassword}"));

			await WebServiceUtility.SendEMailAsync("[Hena Platform] Reseted your password.", msg.ToString(), false, request.EMail);

			var response = new UserModels.ResetPassword.Response();
			return Success(response);
		}
		#endregion // API

		#region Internal Methods
		// -------------------------------------------------------------------------------
		// 이메일 인증코드 검증
		private ErrorCode CheckEMailVerifyCode(string email, string verifyCode, bool checkOnly)
		{
			// check verify code
			var verifyData = VerifyDataManager.Instance.Find(verifyCode);
			if (verifyData == null)
				return ErrorCode.InvalidVerifyCode;

			var verifyEMail = verifyData.UserData as UserModels.SendVerifyEMail.Request;
			if (verifyEMail == null)
				return ErrorCode.InvalidVerifyCode;

			if (email != verifyEMail.EMail)
				return ErrorCode.InvalidVerifyCode;

			if (checkOnly == false)
			{
				if (verifyData.TrySetVerified() == false)
					return ErrorCode.InvalidVerifyCode;
			}

			return ErrorCode.Success;
		}
		#endregion // Internal Methods



	}
}