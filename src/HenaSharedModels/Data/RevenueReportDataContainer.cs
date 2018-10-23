using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class RevenueReportDataContainer
		: ListDataContainer<RevenueReportDataContainer, RevenueReportData>
	{
		public RevenueReportData Find(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public RevenueReportData Find(DBKey userId, DateTime reportDate)
		{
			reportDate = reportDate.Date;
			return Find(item => item.UserId == userId && item.ReportDate.Date == reportDate);
		}

		public RevenueReportData FindOrAdd(DBKey userId, DateTime reportDate)
		{
			var RevenueReport = Find(userId, reportDate);
			if( RevenueReport == null)
			{
				RevenueReport = new RevenueReportData()
				{
					UserId = userId,
					ReportDate = reportDate,
					Revenue = 0m,
					DisplayCount = 0,
					ClickCount = 0,
				};
				Add(RevenueReport);
			}
			return RevenueReport;
		}

		public List<RevenueReportData> FindAll(DBKey userId)
		{
			return FindAll(item => item.UserId == userId);
		}
	}
}
