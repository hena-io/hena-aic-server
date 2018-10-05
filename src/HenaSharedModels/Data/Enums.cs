using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
	public enum SerializationMode
	{
		Normal,
		Simple,
		Full,
		API,
	}

	// 캠페인 타입
	public enum CampaignTypes
	{
		None,
		CPC,	// Click Per Click( 클릭 1회당 과금 )
		CPM,    // Click Per Mille( 노출 1000회당 과금 )
	}

	// 광고 디자인 타입
	public enum AdDesignTypes
	{
		None,
		Banner,			// 배너 광고
		Interstitial,	// 전면 광고
		Video,			// 비디오 광고
	}

	// 유저 권한
	public enum UserPermissionTypes
	{
		None,
		Administrator,  // 관리자 권한
	}

	// 앱 마켓타입
	public enum AppMarketTypes
	{
		None,
		GooglePlay,
		AppleAppStore,
		WebSite,
	}
}
