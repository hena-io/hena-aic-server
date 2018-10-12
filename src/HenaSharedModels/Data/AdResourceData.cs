using System;
using System.Collections.Generic;
using System.Text;

namespace Hena.Shared.Data
{
	// 광고 리소스 데이터
	public class AdResourceData
	{
		// 기본값
		public readonly static AdResourceData Default = new AdResourceData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 광고 리소스 Id
		public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 광고 디자인 타입
		public AdDesignTypes.en AdDesignType { get; set; } = AdDesignTypes.en.None;

		// 리소스 Url
		public string Url { get { return $"http://hena.io/aic/resources/{UserId}/{AdResourceId}"; } }

		// 컨텐트 타입
		public string ContentType { get; set; }

		// 가로 크기
		public short Width { get; set; } = 0;

		// 세로 크기
		public short Height { get; set; } = 0;

		// 생성된 시간
		public DateTime CreateTime { get; set; } = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate { get; set; } = DateTime.MinValue;
	}
}
