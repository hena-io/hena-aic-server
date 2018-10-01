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
    public static class UserBasicDataExtension
	{
		public static bool FromDBTable(this UserBasicData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserDBKey", out item.UserDBKey, UserBasicData.Default.UserDBKey);
			DBUtility.AsValue(row, "EMail", out item.EMail, UserBasicData.Default.EMail);
			DBUtility.AsValue(row, "Password", out item.Password, UserBasicData.Default.Password);
			DBUtility.AsValue(row, "Language", out item.Language, UserBasicData.Default.Language);
			DBUtility.AsValue(row, "TimeZoneId", out item.TimeZoneId, UserBasicData.Default.TimeZoneId);
			DBUtility.AsValue(row, "GivenName", out item.GivenName, UserBasicData.Default.GivenName);
			DBUtility.AsValue(row, "SurName", out item.SurName, UserBasicData.Default.SurName);
			DBUtility.AsValue(row, "RegionCodeForNumber", out item.RegionCodeForNumber, UserBasicData.Default.RegionCodeForNumber);
			DBUtility.AsValue(row, "CountryCode", out item.CountryCode, UserBasicData.Default.CountryCode);
			DBUtility.AsValue(row, "NationalNumber", out item.NationalNumber, UserBasicData.Default.NationalNumber);
			DBUtility.AsValue(row, "IsDeleted", out item.IsDeleted, UserBasicData.Default.IsDeleted);
			DBUtility.AsValue(row, "DeletedTime", out item.DeletedTime, UserBasicData.Default.DeletedTime);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime, UserBasicData.Default.CreateTime);
			DBUtility.AsValue(row, "LastUpdate", out item.LastUpdate, UserBasicData.Default.LastUpdate);
			return true;
		}
		
        public static async Task<bool> FromDBByUserDBKeyAsync(this UserBasicData userBasicData, DBKey userDBKey)
        {
            var query = new DBQuery_User_Select_By_UserDBKey();
            query.IN.DBKey = userDBKey;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref userBasicData);
            return true;
        }

        public static async Task<bool> FromDBByEmailAsync(this UserBasicData userBasicData, string email)
		{
			var query = new DBQuery_User_Select_By_EMail();
			query.IN.EMail = email;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.CopyTo(ref userBasicData);
			return true;
		}

		public static async Task<bool> FromDBByUserNameAsync(this UserBasicData userBasicData, string username)
		{
			var query = new DBQuery_User_Select_By_UserName();
			query.IN.Username = username;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.CopyTo(ref userBasicData);
			return true;
		}
	}
}
