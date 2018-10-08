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
	// 캠페인 데이터
	public class CampaignData
	{
		// 기본값
		public readonly static CampaignData Default = new CampaignData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 캠페인 Id
		public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 캠페인 이름( Max 80 )
		public string Name { get; set; } = string.Empty;

		// 캠페인 타입
		[JsonConverter(typeof(StringEnumConverter))]
		public CampaignTypes CampaignType { get; set; } = CampaignTypes.None;

		// 캠표인 목표( CPC -> 목표 클릭 수, CPM -> 노출 목표 수 )
		public long TargetValue { get; set; } = 0;

		// 캠페인 비용( CPC -> 클릭당 비용, CPM -> 1000노출당 비용 )
		public decimal Cost { get; set; } = 0m;

		// 캠페인 시작 시간
		public DateTime BeginTime { get; set; } = DateTime.MinValue;

		// 캠페인 만료 시간
		public DateTime EndTime { get; set; } = DateTime.MinValue;

		// 캠페인 일시정지 상태
		public bool IsPause { get; set; } = false;

		// 삭제상태 체크
		public bool IsDeleted { get; set; } = false;

		// 삭제된 시간
		public DateTime DeletedTime { get; set; } = DateTime.MinValue;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
		
		
	}
}
