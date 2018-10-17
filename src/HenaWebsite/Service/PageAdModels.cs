using Hena;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HenaWebsite.Models.Service.PageAd
{

	// 광고 정보 데이터
	public class AdInfo
	{
		public AppData AppData { get; set; } = new AppData();
		public AdUnitData AdUnitData { get; set; } = new AdUnitData();
		
		public CampaignData CampaignData { get; set; } = new CampaignData();
		public AdDesignData AdDesignData { get; set; } = new AdDesignData();
		public AdResourceData AdResourceData { get; set; } = new AdResourceData();

		public AdHistoryData AdHistoryData { get; set; } = new AdHistoryData();

		public AdInfo() { }
		public AdInfo(string source)
		{
			this.Decode(source);
		}
	}

	[Serializable]
	public static class PageAdModels
	{
		// 광고 준비
		public static class AdReady
		{
			public class Request
			{
				// 광고 유닛 Id
				public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

				// 클라이언트 타입
				public ClientTypes ClientType { get; set; } = ClientTypes.None;

				// 광고 시스템 타입
				public AdSystemTypes AdSystemType { get; set; } = AdSystemTypes.None;

				// 가로모드인지 체크
				public bool IsLandscape { get; set; } = false;

				// 화면 해상도 가로 사이즈
				public int ScreenWidth { get; set; } = 0;

				// 화면 해상도 세로 사이즈
				public int ScreenHeight { get; set; } = 0;

				public virtual bool IsValidParameters()
				{
					if (AdUnitId == GlobalDefine.INVALID_DBKEY)
						return false;

					if (ScreenWidth <= 0 || ScreenHeight <= 0)
						return false;

					return true;
				}
			}

			public class Response
			{
				// 광고 유닛 Id
				public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

				// 광고 시스템 타입
				public AdSystemTypes AdSystemType { get; set; } = AdSystemTypes.None;

				// 광고 디자인 타입
				public AdDesignTypes.en AdDesignType { get; set; } = AdDesignTypes.en.None;

				// 컨텐츠 타입
				public string ContentType { get; set; } = string.Empty;

				// 리소스 다운로드 Url
				public string ResourceUrl { get; set; } = string.Empty;

				// 가로 크기
				public int Width { get; set; } = 0;

				// 세로 크기
				public int Height { get; set; } = 0;

				// 광고 주소
				public string AdUrl { get; set; } = string.Empty;

				// 광고 클릭시 이동할 주소
				public string AdClickUrl { get; set; } = string.Empty;

				// 광고 정보
				public string Ai { get; set; } = string.Empty;
			}
		}

		// 광고 보여주기
		public static class AdDisplay
		{
			public class Request
			{
				// 광고 정보 데이터( 디스플레이 or 클릭시에 첨부 )
				public string Ai { get; set; } = string.Empty;

				public AdInfo CreateFromAi()
				{
					return new AdInfo().Decode(Ai.DecodeUrlSafeBase64ToBase64());
				}

				public virtual bool IsValidParameters()
				{
					if (StringExtensions.AnyNullOrEmpty(Ai))
						return false;

					return true;
				}
			}
		}

		// 광고 클릭
		public static class AdResource
		{
			public class Request
			{
				// 광고 정보 데이터( 디스플레이 or 클릭시에 첨부 )
				public string Ai { get; set; } = string.Empty;

				public AdInfo CreateFromAi()
				{
					return new AdInfo().Decode(Ai.DecodeUrlSafeBase64ToBase64());
				}


				public virtual bool IsValidParameters()
				{
					if (StringExtensions.AnyNullOrEmpty(Ai))
						return false;

					return true;
				}
			}
		}

		// 광고 클릭
		public static class AdClick
		{
			public class Request
			{
				// 광고 정보 데이터( 디스플레이 or 클릭시에 첨부 )
				public string Ai { get; set; } = string.Empty;

				public AdInfo CreateFromAi()
				{
					return new AdInfo().Decode(Ai.DecodeUrlSafeBase64ToBase64());
				}

				public virtual bool IsValidParameters()
				{
					if (StringExtensions.AnyNullOrEmpty(Ai))
						return false;

					return true;
				}
			}
		}

		
	}

}