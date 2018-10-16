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
	// 광고 기록 데이터
	public class AdHistoryData
	{
		// 기본값
		public readonly static AdHistoryData Default = new AdHistoryData();

		// 광고 기록 Id
		public DBKey AdHistoryId { get; set; } = GlobalDefine.INVALID_DBKEY;
		
		// 퍼블리셔(개발자) Id
		public DBKey PublisherId { get; set; } = GlobalDefine.INVALID_DBKEY;
		// 앱 Id
		public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;
		// 광고 유닛 Id
		public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 광고주 Id
		public DBKey AdvertiserId { get; set; } = GlobalDefine.INVALID_DBKEY;
		// 캠페인 Id
		public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;
		// 광고 디자인 Id
		public DBKey AdDesignId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// IP 주소
		public string IPAddress { get; set; } = string.Empty;
		// User-Agent
		public string UserAgent { get; set; } = string.Empty;

		// 캠페인 타입
		public CampaignTypes CampaignType { get; set; } = CampaignTypes.None;
		// 광고 디자인 타입
		public AdDesignTypes.en AdDesignType { get; set; } = AdDesignTypes.en.None;
		// 광고 비용
		public decimal Cost { get; set; } = 0m;

		// 광고 노출여부
		public bool IsDisplayed { get; set; } = false;
		// 광고 노출시간
		public DateTime DisplayTime { get; set; } = DateTime.MinValue;

		// 광고 클릭여부
		public bool IsClicked { get; set; } = false;
		// 광고 클릭시간
		public DateTime ClickTime { get; set; } = DateTime.MinValue;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;
		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
	}
}
