using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Authorize]
    public class DashboardController : BaseController
	{
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Campaigns()
		{
			return View();
		}

		public IActionResult Apps()
		{
			return View();
		}

		public IActionResult Test()
		{
			return View();
		}
    }
}