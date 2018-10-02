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
        #region UserPermissionData
        public static bool FromDBTable(this UserPermissionData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserDBKey", out item.UserDBKey);
			DBUtility.AsValueEnum(row, "PermissionType", out item.PermissionType);
			DBUtility.AsValue(row, "Level", out item.Level);
			DBUtility.AsValue(row, "RegisterTime", out item.RegisterTime);
			return true;
		}

        public static async Task<bool> FromDBAsync(this UserPermissionData item
            , DBKey userDBKey, UserPermissionTypes permissionType)
        {
            var query = new DBQuery_User_Permission_Select();
            query.IN.UserDBKey = userDBKey;
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

		#region UserData
		public static async Task<bool> FromDBByUserDBKeyAsync(this UserData item, DBKey userDBKey, bool takeBasicData, TimeSpan timeZoneOffset)
		{
            item.TimeZoneOffset = timeZoneOffset;
            DateTime utcNow = DateTime.UtcNow;
            DateTime todayBegin = utcNow.Date;
            DateTime todayEnd = todayBegin.AddDays(1).Subtract(TimeSpan.FromMilliseconds(1));
            DateTime beforeDays31 = todayBegin.Subtract(TimeSpan.FromDays(31));
			List<Task> tasks = new List<Task>();
            if(takeBasicData)
            {
                tasks.Add(item.BasicData.FromDBByEmailAsync(userDBKey));
            }
			tasks.Add(item.Permissions.FromDBByUserDBKeyAsync(userDBKey));

			await Task.WhenAll(tasks.ToArray());
			return Array.TrueForAll(tasks.ToArray(), (Task t) => { return t is Task<bool> ? ((Task<bool>)t).Result : true; });
		}

		public static async Task<bool> FromDBByEmailAsync(this UserData item, string email, TimeSpan timeZoneOffset)
		{
			if (await item.BasicData.FromDBByEmailAsync(email) == false)
				return false;

			return await item.FromDBByUserDBKeyAsync(item.BasicData.UserDBKey, false, timeZoneOffset);
		}
        #endregion // UserData

        #region UserDataContainer
        public static async Task<int> FromDBByLikeEMailAsync(this UserDataContainer item, string email, TimeSpan timeZoneOffset, int offset, int limit = 10)
        {
            item.Clear();

            UserBasicDataContainer basicDataContainer = new UserBasicDataContainer();
            if( await basicDataContainer.FromDBByLikeEMailAsync(email, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach( var it in basicDataContainer.Items)
            {
                var UserData = new UserData();
                UserData.BasicData = it;
                item.Add(UserData);
                tasks.Add(UserData.FromDBByUserDBKeyAsync(it.UserDBKey, false, timeZoneOffset));
            }

            await Task.WhenAll(tasks.ToArray());

            return item.Count;
        }

        public static async Task<int> FromDBByCreateTimeAsync(this UserDataContainer item
            , DateTime beginCreateTime, DateTime endCreateTime, bool sortByCreateTimeDesc, TimeSpan timeZoneOffset, int offset = 0, int limit = 10)
        {
            item.Clear();

            UserBasicDataContainer basicDataContainer = new UserBasicDataContainer();
            if (await basicDataContainer.FromDBByCreateTimeAsync(beginCreateTime, endCreateTime, sortByCreateTimeDesc, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var it in basicDataContainer.Items)
            {
                var UserData = new UserData();
                UserData.BasicData = it;
                item.Add(UserData);
                tasks.Add(UserData.FromDBByUserDBKeyAsync(it.UserDBKey, false, timeZoneOffset));
            }

            await Task.WhenAll(tasks.ToArray());
            return item.Count;
        }
        #endregion  // UserDataContainer
    }
}
