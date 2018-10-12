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
	// 앱 데이터
	public class AdUnitData 
	{
		// 기본값
		public readonly static AdUnitData Default = new AdUnitData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 앱 Id
		public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;
		
		// 광고유닛 Id
		public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 광고유닛 이름( Max 80 )
		public string Name { get; set; } = string.Empty;

		// 광고 시스템 타입
		public AdSystemTypes AdSystemType { get; set; } = AdSystemTypes.None;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
		
	}
}
