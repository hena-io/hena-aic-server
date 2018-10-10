using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum SerializationMode
	{
		Normal,
		Simple,
		Full,
		API,
	}

	// 캠페인 타입
	[JsonConverter(typeof(StringEnumConverter))]
	public enum CampaignTypes
	{
		None,
		CPC,	// Click Per Click( 클릭 1회당 과금 )
		CPM,    // Click Per Mille( 노출 1000회당 과금 )
	}

	// 광고 디자인 타입
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AdDesignTypes
	{
		None,
		Banner,			// 배너 광고
		Interstitial,	// 전면 광고
		Video,			// 비디오 광고
	}

	// 유저 권한
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserPermissionTypes
	{
		None,
		Administrator,  // 관리자 권한
	}

	// 마켓타입
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MarketTypes
	{
		None,
		GooglePlay,
		AppleAppStore,
		WebSite,
	}
}
