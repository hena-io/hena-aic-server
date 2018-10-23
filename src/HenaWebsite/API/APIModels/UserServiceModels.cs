using Hena;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.UserService
{
	public static class UserServiceModels
	{
		// 잔고
		public static class Balances
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
			}

			public class Response
			{
				public List<BalanceData> Balances = new List<BalanceData>();
			}
		}

		// AIC 기록
		public static class AICHistory
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				// false : 고객 Id로 검색, true : 퍼블리셔 Id로 검색
				public bool IsPublisherReport { get; set; } = false;
				public int Offset { get; set; } = 0;
				public int Count { get; set; } = 20;
			}

			public class Response
			{
				public List<AdHistoryData> Items = new List<AdHistoryData>();
			}
		}

		// AIC 리포트
		public static class AICRevenueReport
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				// false : 고객 Id로 검색, true : 퍼블리셔 Id로 검색
				public bool IsPublisherReport { get; set; } = false;
				public DateTime BeginTime { get; set; } = DateTime.MinValue;
				public DateTime EndTime { get; set; } = DateTime.MinValue;
				public TimeSpan TimeZoneOffset { get; set; } = TimeSpan.Zero;
			}

			public class Response
			{
				public List<RevenueReportData> Items = new List<RevenueReportData>();
			}
		}
	}
}
