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
	// 마이닝 기록 데이터
	public class MiningHistoryData
	{
		// 기본값
		public readonly static MiningHistoryData Default = new MiningHistoryData();

		// 마이닝 기록 Id
		public DBKey MiningHistoryId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 재화 타입
		public CurrencyTypes CurrencyType { get; set; } = CurrencyTypes.None;

		// 마이닝 갯수
		public decimal MiningAmount { get; set; } = 0m;

		// 마이닝된 시간
		public DateTime MiningTime { get; set; } = DateTime.MinValue;
	}
}
