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
    public static class AppDataExtension
	{
		public static bool FromDBTable(this AppData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserDBKey", out item.UserDBKey, AppData.Default.UserDBKey);
			DBUtility.AsValue(row, "AppDBKey", out item.AppDBKey, AppData.Default.AppDBKey);
			DBUtility.AsValue(row, "Name", out item.Name, AppData.Default.Name);
			DBUtility.AsValue(row, "AppMarketType", out item.AppMarketType, AppData.Default.AppMarketType);
			DBUtility.AsValue(row, "IsDeleted", out item.IsDeleted, AppData.Default.IsDeleted);
			DBUtility.AsValue(row, "DeletedTime", out item.DeletedTime, AppData.Default.DeletedTime);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime, AppData.Default.CreateTime);
			DBUtility.AsValue(row, "LastUpdate", out item.LastUpdate, AppData.Default.LastUpdate);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AppData item, DBKey AppDBKey)
        {
            var query = new DBQuery_App_Select();
            query.IN.DBKey = AppDBKey;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref item);
            return true;
        }
	}
}
