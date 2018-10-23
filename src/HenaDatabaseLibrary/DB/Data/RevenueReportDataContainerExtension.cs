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
    public static class RevenueReportDataContainerExtension
	{
		public static async Task<int> FromDBByPublisherIdAsync(this RevenueReportDataContainer item, DBKey userId, DateTime beginTimeLocal, DateTime endTimeLocal, TimeSpan timeOffset)
		{
			var query = new DBQuery_RevenueReport_Select_By_PublisherId();
			query.IN.UserId = userId;
			query.IN.BeginTimeLocal = beginTimeLocal;
			query.IN.EndTimeLocal = endTimeLocal;
			query.IN.TimeOffset = timeOffset;

			bool result = await DBThread.Instance.ReqQueryAsync(query);
			query.OUT.Items.CopyTo(ref item);
			return item.Count;
		}

		public static async Task<int> FromDBByCustomerIdAsync(this RevenueReportDataContainer item, DBKey userId, DateTime beginTimeLocal, DateTime endTimeLocal, TimeSpan timeOffset)
		{
			var query = new DBQuery_RevenueReport_Select_By_CustomerId();
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
