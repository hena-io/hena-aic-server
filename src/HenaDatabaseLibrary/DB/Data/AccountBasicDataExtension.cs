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
    public static class AccountBasicDataExtension
	{
		public static bool FromDBTable(this AccountBasicData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "AccountDBKey", out item.AccountDBKey);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime);
			DBUtility.AsValue(row, "GivenName", out item.GivenName);
			DBUtility.AsValue(row, "SurName", out item.SurName);
			DBUtility.AsValue(row, "Username", out item.Username);
			DBUtility.AsValue(row, "EMail", out item.EMail);
			DBUtility.AsValue(row, "Password", out item.Password);
			DBUtility.AsValue(row, "RegionCodeForNumber", out item.RegionCodeForNumber);
			DBUtility.AsValue(row, "CountryCode", out item.CountryCode);
            DBUtility.AsValue(row, "NationalNumber", out item.NationalNumber);
			return true;
		}
		
        public static async Task<bool> FromDBByAccountDBKeyAsync(this AccountBasicData accountBasicData, DBKey accountDBKey)
        {
            var query = new DBQuery_Account_Select_By_AccountDBKey();
            query.IN.DBKey = accountDBKey;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref accountBasicData);
            return true;
        }

        public static async Task<bool> FromDBByEmailAsync(this AccountBasicData accountBasicData, string email)
		{
			var query = new DBQuery_Account_Select_By_EMail();
			query.IN.EMail = email;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.CopyTo(ref accountBasicData);
			return true;
		}

		public static async Task<bool> FromDBByUserNameAsync(this AccountBasicData accountBasicData, string username)
		{
			var query = new DBQuery_Account_Select_By_UserName();
			query.IN.Username = username;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.CopyTo(ref accountBasicData);
			return true;
		}
	}
}
