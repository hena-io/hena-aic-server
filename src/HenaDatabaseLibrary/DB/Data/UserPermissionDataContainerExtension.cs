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
    public static class UserPermissionDataContainerExtension
	{
        
        #region UserPermissionDataContainer
        public static async Task<int> FromDBByUserDBKeyAsync(this UserPermissionDataContainer item, DBKey userDBKey)
		{
			var query = new DBQuery_User_Permission_Select_By_UserDBKey();
			query.IN.DBKey = userDBKey;
			var result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
		#endregion // UserPermissionDataContainer
    }
}
