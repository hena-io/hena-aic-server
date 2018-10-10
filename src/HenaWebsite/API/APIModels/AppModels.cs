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

namespace HenaWebsite.Models.API.App
{
	[Serializable]
	public class AppModels
	{
		// 앱 생성
		public static class Create
		{
			public class Request
			{
				public string Name { get; set; } = string.Empty;
				public MarketTypes MarketType { get; set; } = MarketTypes.None;

				public virtual bool IsValidParameters()
				{
					var name = Name.Trim();
					if (string.IsNullOrEmpty(name) || name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : AppData
			{
			}
		}

		// 앱 수정
		public static class Modify
		{
			public class Request : Create.Request
			{
				public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public override bool IsValidParameters()
				{
					if (AppId.IsValid() == false)
						return false;

					return base.IsValidParameters();
				}
			}

			public class Response : AppData
			{
			}
		}

		// 앱 삭제
		public static class Delete
		{
			public class Request
			{
				public DBKey AppId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AppId <= 0)
						return false;

					return true;
				}
			}
		}

		// 앱 목록
		public static class List
		{
			public class Response
			{
				public AppData[] Apps { get; set; }
			}
		}

	}
}