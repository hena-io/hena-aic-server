using Hena;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.AdUnit
{
	public static class AdUnitModels
	{
		// 광고 유닛 생성
		public static class Create
		{
			public class Request
			{
				public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public string Name { get; set; } = string.Empty;
				public AdDesignTypes AdDesignType { get; set; } = AdDesignTypes.None;

				public virtual bool IsValidParameters()
				{
					if (AppId.IsValid() == false)
						return false;

					if (string.IsNullOrEmpty(Name) || Name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : AdUnitData
			{
			}
		}

		// 광고 유닛 수정
		public static class Modify
		{
			public class Request
			{
				public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public string Name { get; set; } = string.Empty;
				public AdDesignTypes AdDesignType { get; set; } = AdDesignTypes.None;

				public virtual bool IsValidParameters()
				{
					if (AdUnitId.IsValid() == false)
						return false;

					if (string.IsNullOrEmpty(Name) || Name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : AdUnitData
			{
			}
		}

		// 광고 유닛 삭제
		public static class Delete
		{
			public class Request
			{
				public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AdUnitId.IsValid() == false)
						return false;

					return true;
				}
			}
		}

		// 광고 유닛 목록
		public static class List
		{
			public class Request
			{
				public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AppId.IsValid() == false)
						return false;

					return true;
				}
			}

			public class Response
			{
				public AdUnitData[] AdUnits { get; set; }
			}
		}
	}
}