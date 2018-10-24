using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class MiningReportDataContainer
		: ListDataContainer<MiningReportDataContainer, MiningReportData>
	{
		public MiningReportData Find(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public MiningReportData Find(DBKey userId, DateTime reportDate)
		{
			reportDate = reportDate.Date;
			return Find(item => item.UserId == userId && item.ReportDate.Date == reportDate);
		}

		public MiningReportData FindOrAdd(DBKey userId, DateTime reportDate)
		{
			var MiningReport = Find(userId, reportDate);
			if( MiningReport == null)
			{
				MiningReport = new MiningReportData()
				{
					UserId = userId,
					ReportDate = reportDate,
					MiningAmount = 0m,
				};
				Add(MiningReport);
			}
			return MiningReport;
		}

		public List<MiningReportData> FindAll(DBKey userId)
		{
			return FindAll(item => item.UserId == userId);
		}
	}
}
