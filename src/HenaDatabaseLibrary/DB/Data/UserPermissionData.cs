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
    public static class UserPermissionDataExtension
	{
        #region UserPermissionData
        public static bool FromDBTable(this UserPermissionData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserId", out item.UserId);
			DBUtility.AsValueEnum(row, "PermissionType", out item.PermissionType);
			DBUtility.AsValue(row, "Level", out item.Level);
			DBUtility.AsValue(row, "RegisterTime", out item.RegisterTime);
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
                permissionData.CopyTo(ref item);
                return true;
            }
            return false;
        }
        #endregion // UserPermissionData
    }
}
