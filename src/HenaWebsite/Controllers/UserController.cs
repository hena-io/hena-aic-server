using System;
using System.Collections.Generic;
using System.Linq;
using Hena.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hena;
using Hena.DB;
using Hena.Shared.Data;
using HenaWebsite.Models;
using HenaWebsite.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HenaWebsite.Controllers
{
	public class UserController : BaseController
	{
		#region View
		[HttpGet]
		public IActionResult Login()
        {
			if (User.Identity.IsAuthenticated)
			{
				return Redirect(Url.Action("Index", "Dashboard"));
			}

			return View();
		}

		// 로그인 요청
		[HttpPost]
		public async Task<IActionResult> Login(LoginFormModel model)
		{
			ErrorCode errorCode = await ProcessLoginAsync(model.EMail, model.Password, model.TimeZoneOffsetFromUTC);

			ViewData["ErrorCode"] = errorCode;

			if( errorCode == ErrorCode.Success )
			{
				return Redirect(Url.Action("Index", "Dashboard"));
			}
			return View();
		}

		// 로그인 처리
		private async Task<ErrorCode > ProcessLoginAsync(string email, string password, int timeZoneOffsetFromUTC = 0)
		{
			if (User.Identity.IsAuthenticated)
			{
				return ErrorCode.AlreadyLoggedin;
			}

			if (email.IsValidEmailAddress() == false)
				return ErrorCode.InvalidEMail;

			UserBasicData basicData = new UserBasicData();
			if (await basicData.FromDBByEmailAsync(email) == false)
				return ErrorCode.InvalidEMail;

			if (PasswordUtility.VerifyPassword(password, basicData.Password) == false)
				return ErrorCode.InvalidPassword;

			var claims = new List<Claim>();
			claims.Add(new Claim(HenaClaimTypes.UserId, basicData.UserId.ToString()));
			claims.Add(new Claim(HenaClaimTypes.GivenName, basicData.GivenName));
			claims.Add(new Claim(HenaClaimTypes.Surname, basicData.SurName));
			claims.Add(new Claim(HenaClaimTypes.Email, basicData.EMail));
			claims.Add(new Claim(HenaClaimTypes.Language, basicData.Language));
			claims.Add(new Claim(HenaClaimTypes.TimeZoneId, basicData.TimeZoneId));
			claims.Add(new Claim(HenaClaimTypes.TimeZoneOffset, TimeSpan.FromMinutes(timeZoneOffsetFromUTC).ToString()));

			var userIdentity = new ClaimsIdentity(claims, "login");

			ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
			await HttpContext.SignInAsync(principal);

			Response.Cookies.Delete(HenaClaimTypes.UserId);
			Response.Cookies.Append(HenaClaimTypes.UserId, basicData.UserId.ToString());


			return ErrorCode.Success;
		}

		public async Task<IActionResult> Logout()
		{
			await SignOutAsync();
			return WebServiceUtility.RedirectToActionWithNonce(this, "Index", "Home");
		}

		public IActionResult SignUp()
		{
			return View();
		}

		public IActionResult ForgotPassword()
		{
			return View();
		}

		[Authorize, HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}

		[Authorize, HttpPost]
		public async Task<IActionResult> ChangePassword(string password, string newPassword, string confirmPassword)
		{
			if (string.IsNullOrEmpty(password)
				|| string.IsNullOrEmpty(newPassword)
				|| newPassword.Length < 8
				|| newPassword != confirmPassword)
			{
				ModelState.AddModelError("Message", "Invalid Password");
				return View();
			}

			UserBasicData basicData = new UserBasicData();
			if( await basicData.FromDBAsync(UserId) == false )
			{
				ModelState.AddModelError("Message", "Invalid Session");
				return View();
			}

			if (PasswordUtility.VerifyPassword(password, basicData.Password) == false)
			{
				ModelState.AddModelError("Message", "Invalid Password");
				return View();
			}

			var query = new DBQuery_User_Update_Password();
			query.IN.UserId = UserId;
			query.IN.Password = PasswordUtility.HashPassword(newPassword);

			await DBThread.Instance.ReqQueryAsync(query);

			return RedirectToAction("Index", "Dashboard");
		}
		#endregion // View
	}
}