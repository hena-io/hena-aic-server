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
    public static class AdUnitDataExtension
	{
		public static bool FromDBTable(this AdUnitData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserDBKey", out item.UserDBKey, AdUnitData.Default.UserDBKey);
			DBUtility.AsValue(row, "AppDBKey", out item.AppDBKey, AdUnitData.Default.AppDBKey);
			DBUtility.AsValue(row, "AdUnitDBKey", out item.AdUnitDBKey, AdUnitData.Default.AdUnitDBKey);
			DBUtility.AsValue(row, "Name", out item.Name, AdUnitData.Default.Name);
			DBUtility.AsValue(row, "AdDesignType", out item.AdDesignType, AdUnitData.Default.AdDesignType);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime, AdUnitData.Default.CreateTime);
			DBUtility.AsValue(row, "LastUpdate", out item.LastUpdate, AdUnitData.Default.LastUpdate);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AdUnitData item, DBKey AppDBKey)
        {
            var query = new DBQuery_AdUnit_Select();
            query.IN.DBKey = AppDBKey;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref item);
            return true;
        }
	}
}
