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
    public static class UserPermissionDataExtension
	{
        #region UserPermissionData
        public static bool FromDBTable(this UserPermissionData item, DataRow row)
		{
			if (row == null)
				return false;

			row.Copy(item);
			return true;
		}

        public static async Task<bool> FromDBAsync(this UserPermissionData item
            , DBKey userId, UserPermissionTypes permissionType)
        {
            var query = new DBQuery_User_Permission_Select();
            query.IN.UserId = userId;
            query.IN.PermissionType = permissionType;
            var result = await DBThread.Instance.ReqQueryAsync(query);
            var permissionData = query.OUT.Items.Find(permissionType);
            if(permissionData != null)
            {
                permissionData.Copy(item);
                return true;
            }
            return false;
        }
        #endregion // UserPermissionData
    }
}
