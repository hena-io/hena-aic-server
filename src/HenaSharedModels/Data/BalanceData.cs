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
	// 잔고 데이터
	public class BalanceData 
	{
		// 기본값
		public readonly static BalanceData Default = new BalanceData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 재화 타입
		public CurrencyTypes CurrencyType { get; set; } = CurrencyTypes.None;

		// 잔고
		public decimal Balance { get; set; } = 0;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
		
	}
}
