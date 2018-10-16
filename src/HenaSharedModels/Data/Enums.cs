using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
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

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ClientTypes
	{
		None,
		Android,
		IOS,
		Web,
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
	public struct AdDesignTypes
	{
		#region Caching
		static Dictionary<en, Size> ENUM_SIZE_MAP= null;
		static Dictionary<en, Size> ENUM_HDSIZE_MAP = null;
		static Dictionary<Size, en> SIZE_ENUM_MAP = null;
		static Dictionary<Size, en> HDSIZE_ENUM_MAP = null;
		private static void BuildMap()
		{
			if (ENUM_SIZE_MAP == null)
				ENUM_SIZE_MAP = MakeEnumSizeMap(false);

			if (ENUM_HDSIZE_MAP == null)
				ENUM_HDSIZE_MAP = MakeEnumSizeMap(true);

			if (SIZE_ENUM_MAP == null)
				SIZE_ENUM_MAP = MakeSizeEnumMap(false);

			if (HDSIZE_ENUM_MAP == null)
				HDSIZE_ENUM_MAP = MakeSizeEnumMap(true);
		}

		static Dictionary<en, Size> MakeEnumSizeMap(bool isHD)
		{
			int mul = isHD ? 2 : 1;

			var map = new Dictionary<en, Size>();
			map.Add(en.MobileLeaderboard, new Size(320, 50) * mul);
			map.Add(en.MobileBannerLandscape, new Size(480, 32) * mul);
			map.Add(en.LargeMobileBanner, new Size(320, 100) * mul);
			map.Add(en.Banner, new Size(468, 60) * mul);
			map.Add(en.Leaderboard, new Size(728, 90) * mul);
			map.Add(en.InlineRectangle, new Size(300, 250) * mul);
			map.Add(en.SmartphoneInterstitialPortrait, new Size(320, 480) * mul);
			map.Add(en.SmartphoneInterstitialLandscape, new Size(480, 320) * mul);
			map.Add(en.TabletInterstitialPortrait, new Size(768, 1024) * mul);
			map.Add(en.TabletInterstitialLandscape, new Size(1024, 768) * mul);
			return map;
		}

		static Dictionary<Size, en> MakeSizeEnumMap(bool isHD)
		{
			Dictionary<Size, en> sizeMap = new Dictionary<Size, en>();
			var enumMap = MakeEnumSizeMap(isHD);
			foreach (var it in enumMap)
			{
				sizeMap.Add(it.Value, it.Key);
			}
			return sizeMap;
		}
		#endregion // Caching

		[JsonConverter(typeof(StringEnumConverter))]
		public enum en
		{
			None,
				
			MobileLeaderboard,					// 320 x 50: 모바일 리더보드
			MobileBannerLandscape,				// 480 x 32: 모바일 배너(가로 모드)
			LargeMobileBanner,					// 320 x 100: 큰 모바일 배너
			Banner,								// 468 x 60: 배너
			Leaderboard,						// 728 x 90: 리더보드
			InlineRectangle,					// 300 x 250: 인라인 직사각형
			SmartphoneInterstitialPortrait,		// 320 x 480: 스마트폰 전면 광고(세로 모드)
			SmartphoneInterstitialLandscape,	// 480 x 320: 스마트폰 전면 광고(가로 모드)
			TabletInterstitialPortrait,			// 768 x 1024: 태블릿 전면 광고(세로 모드)
			TabletInterstitialLandscape,		// 1024 x 768: 태블릿 전면 광고(가로 모드)
		}

		public static en[] ToSupported(ClientTypes clientType, AdSystemTypes adSystemType, bool isLandscape)
		{
			if (clientType == ClientTypes.None)
				return new en[0];

			if (clientType == ClientTypes.Web)
				return new en[] { en.Leaderboard, en.InlineRectangle };

			List<en> items = new List<en>();
			if (adSystemType == AdSystemTypes.Banner)
			{
				if (isLandscape)
				{
					items.Add(en.MobileBannerLandscape);
				}
				else
				{
					items.Add(en.MobileLeaderboard);
					items.Add(en.LargeMobileBanner);
				}
				items.Add(en.Banner);
			}
			else if (adSystemType == AdSystemTypes.Interstitial)
			{
				if (isLandscape)
				{
					items.Add(en.MobileBannerLandscape);
				}
				else
				{
					items.Add(en.MobileLeaderboard);
					items.Add(en.LargeMobileBanner);
				}
			}

			return items.ToArray();
		}

		public static Size ToSize(en e)
		{
			BuildMap();
			Size outValue = Size.Empty;
			if (ENUM_SIZE_MAP.TryGetValue(e, out outValue))
				return outValue;

			return Size.Empty;
		}

		public static Size ToHDSize(en e)
		{
			BuildMap();
			Size outValue = Size.Empty;
			if (ENUM_HDSIZE_MAP.TryGetValue(e, out outValue))
				return outValue;

			return Size.Empty;
		}

		public static en ToEnum(Size size)
		{
			BuildMap();
			en outValue = en.None;
			if (SIZE_ENUM_MAP.TryGetValue(size, out outValue))
				return outValue;

			if (HDSIZE_ENUM_MAP.TryGetValue(size, out outValue))
				return outValue;

			return en.None;
		}
	}

	// 광고 시스템 타입
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AdSystemTypes
	{
		None,
		Banner,         // 배너 광고
		Interstitial,   // 전면 광고
		Video,          // 비디오 광고
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

	// 광고 리소스 타입
	[JsonConverter(typeof(StringEnumConverter))]
	public enum AdResourceTypes
	{
		None,
		Image,
		Video,
	}
}
