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
    public static class AdResourceDataExtension
	{
		public static bool FromDBTable(this AdResourceData item, DataRow row)
		{
			if (row == null)
				return false;

			row.Copy(item);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AdResourceData item, DBKey adResourceId)
        {
            var query = new DBQuery_AdResource_Select();
            query.IN.DBKey = adResourceId;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.Copy(item);
            return true;
        }
	}
}
