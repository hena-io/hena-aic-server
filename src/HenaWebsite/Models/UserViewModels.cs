using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.User
{
	// Login
	public class LoginFormModel
	{
		public string EMail { get; set; }
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

	
}
