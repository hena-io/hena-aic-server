﻿using System;
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
	public class AppData
	{
		// 기본값
		public readonly static AppData Default = new AppData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		//  Id
		public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 앱 이름( Max 80 )
		public string Name { get; set; } = string.Empty;

		// 마켓 타입
		public MarketTypes MarketType { get; set; } = MarketTypes.None;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
	}
}
