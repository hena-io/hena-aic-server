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
	public class MiningReportData
	{
		// 기본값
		public readonly static MiningReportData Default = new MiningReportData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 채굴량
		public decimal MiningAmount { get; set; } = 0;

		// 리포트 날짜( LocalTime )
		public DateTime ReportDate { get; set; } = DateTime.MinValue;
	}
}
