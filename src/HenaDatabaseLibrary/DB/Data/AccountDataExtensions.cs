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
    public static class AccountDataExtension
	{
        #region AccountPermissionData
        public static bool FromDBTable(this AccountPermissionData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "AccountDBKey", out item.AccountDBKey);
			DBUtility.AsValueEnum(row, "PermissionType", out item.PermissionType);
			DBUtility.AsValue(row, "Level", out item.Level);
			DBUtility.AsValue(row, "RegisterTime", out item.RegisterTime);
			return true;
		}

        public static async Task<bool> FromDBAsync(this AccountPermissionData item
            , DBKey accountDBKey, AccountPermissionType permissionType)
        {
            var query = new DBQuery_Account_Permission_Select();
            query.IN.AccountDBKey = accountDBKey;
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
        #endregion // AccountPermissionData

        #region AccountPermissionDataContainer
        public static async Task<int> FromDBByAccountDBKeyAsync(this AccountPermissionDataContainer item, DBKey accountDBKey)
		{
			var query = new DBQuery_Account_Permission_Select_By_AccountDBKey();
			query.IN.DBKey = accountDBKey;
			var result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
		#endregion // AccountPermissionDataContainer

		#region AccountData
		public static async Task<bool> FromDBByAccountDBKeyAsync(this AccountData item, DBKey accountDBKey, bool takeBasicData, TimeSpan timeZoneOffset)
		{
            item.TimeZoneOffset = timeZoneOffset;
            DateTime utcNow = DateTime.UtcNow;
            DateTime todayBegin = utcNow.Date;
            DateTime todayEnd = todayBegin.AddDays(1).Subtract(TimeSpan.FromMilliseconds(1));
            DateTime beforeDays31 = todayBegin.Subtract(TimeSpan.FromDays(31));
			List<Task> tasks = new List<Task>();
            if(takeBasicData)
            {
                tasks.Add(item.BasicData.FromDBByEmailAsync(accountDBKey));
            }
			tasks.Add(item.Permissions.FromDBByAccountDBKeyAsync(accountDBKey));

			await Task.WhenAll(tasks.ToArray());
			return Array.TrueForAll(tasks.ToArray(), (Task t) => { return t is Task<bool> ? ((Task<bool>)t).Result : true; });
		}

		public static async Task<bool> FromDBByEmailAsync(this AccountData item, string email, TimeSpan timeZoneOffset)
		{
			if (await item.BasicData.FromDBByEmailAsync(email) == false)
				return false;

			return await item.FromDBByAccountDBKeyAsync(item.BasicData.AccountDBKey, false, timeZoneOffset);
		}
        #endregion // AccountData

        #region AccountDataContainer
        public static async Task<int> FromDBByLikeEMailAsync(this AccountDataContainer item, string email, TimeSpan timeZoneOffset, int offset, int limit = 10)
        {
            item.Clear();

            AccountBasicDataContainer basicDataContainer = new AccountBasicDataContainer();
            if( await basicDataContainer.FromDBByLikeEMailAsync(email, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach( var it in basicDataContainer.Items)
            {
                var accountData = new AccountData();
                accountData.BasicData = it;
                item.Add(accountData);
                tasks.Add(accountData.FromDBByAccountDBKeyAsync(it.AccountDBKey, false, timeZoneOffset));
            }

            await Task.WhenAll(tasks.ToArray());

            return item.Count;
        }

        public static async Task<int> FromDBByCreateTimeAsync(this AccountDataContainer item
            , DateTime beginCreateTime, DateTime endCreateTime, bool sortByCreateTimeDesc, TimeSpan timeZoneOffset, int offset = 0, int limit = 10)
        {
            item.Clear();

            AccountBasicDataContainer basicDataContainer = new AccountBasicDataContainer();
            if (await basicDataContainer.FromDBByCreateTimeAsync(beginCreateTime, endCreateTime, sortByCreateTimeDesc, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var it in basicDataContainer.Items)
            {
                var accountData = new AccountData();
                accountData.BasicData = it;
                item.Add(accountData);
                tasks.Add(accountData.FromDBByAccountDBKeyAsync(it.AccountDBKey, false, timeZoneOffset));
            }

            await Task.WhenAll(tasks.ToArray());
            return item.Count;
        }
        #endregion  // AccountDataContainer
    }
}
