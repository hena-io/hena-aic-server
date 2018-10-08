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
    public static class AdUnitDataExtension
	{
		public static bool FromDBTable(this AdUnitData item, DataRow row)
		{
			if (row == null)
				return false;

			row.Copy(item);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AdUnitData item, DBKey AppId)
        {
            var query = new DBQuery_AdUnit_Select();
            query.IN.DBKey = AppId;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.Copy(item);
            return true;
        }
	}
}
