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
	// 광고 디자인 데이터
	public class AdDesignData
	{
		// 기본값
		public readonly static AdDesignData Default = new AdDesignData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 캠페인 Id
		public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 광고 디자인 Id
		public DBKey AdDesignId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 디자인 이름( Max 80 )
		public string Name { get; set; } = string.Empty;

		// 광고 디자인 타입
		public AdDesignTypes AdDesignType { get; set; } = AdDesignTypes.None;

		// 업로드된 리소스 Id
		public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 업로드된 리소스 Url
		public string AdResourceUrl { get { return $"http://hena.io/aic/resources/{UserId}/{AdResourceId}"; } }

		// 목적지 URL
		public string DestinationUrl { get; set; } = string.Empty;

		// 캠페인 일시정지 상태
		public bool IsPause { get; set; } = false;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
	}
}
