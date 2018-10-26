using Hena;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.MiningModel
{
	public static class MiningModels
	{
		// Mining 시작
		public static class MiningStart
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
			}
		}

		// Mining 정지
		public static class MiningStop
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
			}
		}

		// 세션 업데이트
		public static class UpdateSession
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
			}
		}

		// Mining 기록
		public static class MiningHistory
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public int Offset { get; set; } = 0;
				public int Count { get; set; } = 20;
			}

			public class Response
			{
				public List<MiningHistoryData> Items = new List<MiningHistoryData>();
			}
		}

		// Mining 리포트
		public static class MiningReport
		{
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public DateTime BeginTime { get; set; } = DateTime.MinValue;
				public DateTime EndTime { get; set; } = DateTime.MinValue;
				public TimeSpan TimeZoneOffset { get; set; } = TimeSpan.Zero;
			}

			public class Response
			{
				public List<MiningReportData> Items = new List<MiningReportData>();
			}
		}
	}
}
