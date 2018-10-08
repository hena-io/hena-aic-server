using Hena.DB;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
    public static class UserPermissionDataContainerExtension
	{
        
        #region UserPermissionDataContainer
        public static async Task<int> FromDBByUserIdAsync(this UserPermissionDataContainer item, DBKey userId)
		{
			var query = new DBQuery_User_Permission_Select_By_UserId();
			query.IN.DBKey = userId;
			var result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.Copy(item);
			return item.Count;
		}
		#endregion // UserPermissionDataContainer
    }
}
