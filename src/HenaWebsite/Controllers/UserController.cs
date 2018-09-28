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
using Microsoft.AspNetCore.Mvc;

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
			ErrorCode errorCode = await ProcessLoginAsync(model.EmailOrUserName, model.Password, model.TimeZoneOffsetFromUTC);

			ViewData["ErrorCode"] = errorCode;

			if( errorCode == ErrorCode.Success )
			{
				return Redirect(Url.Action("Index", "Dashboard"));
			}
			return View();
		}

		// 로그인 처리
		private async Task<ErrorCode > ProcessLoginAsync(string emailOrUserName, string password, int timeZoneOffsetFromUTC)
		{
			if (User.Identity.IsAuthenticated)
			{
				return ErrorCode.AlreadyLoggedin;
			}

			if (emailOrUserName.Length < 2)
				return ErrorCode.InvalidUserName;

			bool isEMail = emailOrUserName.Contains('@');

			AccountBasicData basicData = new AccountBasicData();
			if (isEMail)
			{
				if (await basicData.FromDBByEmailAsync(emailOrUserName) == false)
					return ErrorCode.InvalidEMail;
			}
			else
			{
				if (await basicData.FromDBByUserNameAsync(emailOrUserName) == false)
					return ErrorCode.InvalidUserName;
			}

			if (PasswordUtility.VerifyPassword(password, basicData.Password) == false)
				return ErrorCode.InvalidPassword;

			var claims = new List<Claim>();
			claims.Add(new Claim(ClaimTypes.SerialNumber, basicData.AccountDBKey.ToString()));
			claims.Add(new Claim(ClaimTypes.GivenName, basicData.GivenName));
			claims.Add(new Claim(ClaimTypes.Surname, basicData.SurName));
			claims.Add(new Claim(ClaimTypes.Name, basicData.Username));
			claims.Add(new Claim(ClaimTypes.Email, basicData.EMail));
			claims.Add(new Claim("TimeZoneOffset", TimeSpan.FromMinutes(timeZoneOffsetFromUTC).ToString()));

			var userIdentity = new ClaimsIdentity(claims, "login");

			ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
			await HttpContext.SignInAsync(principal);

			return ErrorCode.Success;
		}

		public async Task<IActionResult> Logout()
		{
		
			await HttpContext.SignOutAsync();

			foreach (var cookie in Request.Cookies.Keys)
			{
				Response.Cookies.Delete(cookie);
			}
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
		#endregion // View
	}
}