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
    public static class UserBasicDataExtension
	{
		public static bool FromDBTable(this UserBasicData item, DataRow row)
		{
			if (row == null)
				return false;

			row.Copy(item);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this UserBasicData userBasicData, DBKey userId)
        {
            var query = new DBQuery_User_Select();
            query.IN.DBKey = userId;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;


            query.OUT.FirstItem.Copy(userBasicData);
            return true;
        }

        public static async Task<bool> FromDBByEmailAsync(this UserBasicData userBasicData, string email)
		{
			var query = new DBQuery_User_Select_By_EMail();
			query.IN.EMail = email;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.Copy(userBasicData);
			return true;
		}
	}
}
