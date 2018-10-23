using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 수익 리포트 데이터
	public class RevenueReportData
	{
		// 기본값
		public readonly static RevenueReportData Default = new RevenueReportData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 수익금
		public decimal Revenue { get; set; } = 0;

		// 노출 횟수
		public int DisplayCount { get; set; } = 0;

		// 클릭 횟수
		public int ClickCount { get; set; } = 0;

		// 리포트 날짜( LocalTime )
		public DateTime ReportDate { get; set; } = DateTime.MinValue;
	}
}
