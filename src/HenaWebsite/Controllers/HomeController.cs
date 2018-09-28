using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HenaWebsite.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace HenaWebsite.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IStringLocalizer<HomeController> _localizer;

		public HomeController(IStringLocalizer<HomeController> localizer)
		{
			_localizer = localizer;
		}

		public IActionResult Index()
		{
			return RedirectToAction("Login", "User");
			//string value = _localizer["TEST"].Value;
			//return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
