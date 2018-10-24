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
    public static class MiningReportDataContainerExtension
	{
		public static async Task<int> FromDBByUserIdAsync(this MiningReportDataContainer item, DBKey userId, DateTime beginTimeLocal, DateTime endTimeLocal, TimeSpan timeOffset)
		{
			var query = new DBQuery_MiningReport_Select_By_UserId();
			query.IN.UserId = userId;
			query.IN.BeginTimeLocal = beginTimeLocal;
			query.IN.EndTimeLocal = endTimeLocal;
			query.IN.TimeOffset = timeOffset;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}
	}
}
