using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HenaWebsite.Models.Campaigns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Authorize]
    public class CampaignsController : BaseController
	{
        public IActionResult Index()
        {
			return Redirect(Url.Action("Reports", "Campaigns"));
        }

		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Reports()
		{
			return View();
		}

		#region API
		[HttpPost]
		public async Task<IActionResult> CreateCampaign([FromBody] APIReqeust_CreateCampaign model)
		{
			return Success();
		}
		#endregion	// API

	}
}