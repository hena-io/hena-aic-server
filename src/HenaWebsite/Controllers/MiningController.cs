using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HenaWebsite.Controllers
{
	[Authorize]
    public class MiningController : BaseController
	{
        public IActionResult Report()
        {
			return View();
        }
	}
}