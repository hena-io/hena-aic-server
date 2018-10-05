using Hena.DB;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
    public static class UserDataExtension
	{
		#region UserData
		public static async Task<bool> FromDBByUserDBKeyAsync(this UserData item, DBKey userDBKey, bool takeBasicData)
		{
			List<Task> tasks = new List<Task>();
            if(takeBasicData)
            {
                tasks.Add(item.BasicData.FromDBByEmailAsync(userDBKey));
            }
			tasks.Add(item.Permissions.FromDBByUserDBKeyAsync(userDBKey));

			await Task.WhenAll(tasks.ToArray());
			return Array.TrueForAll(tasks.ToArray(), (Task t) => { return t is Task<bool> ? ((Task<bool>)t).Result : true; });
		}

		public static async Task<bool> FromDBByEmailAsync(this UserData item, string email)
		{
			if (await item.BasicData.FromDBByEmailAsync(email) == false)
				return false;

			return await item.FromDBByUserDBKeyAsync(item.BasicData.UserDBKey, false);
		}
        #endregion // UserData
    }
}
