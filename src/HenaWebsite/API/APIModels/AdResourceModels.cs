using Hena;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.AdResource
{
	public static class AdResourceModels
	{
		// 광고 리소스 업로드
		public static class Upload
		{
			public class Request
			{
				public IFormFile File { get; set; }

				public virtual bool IsValidParameters()
				{
					return File != null;
				}
			}

			public class Response : AdResourceData
			{
			}
		}

		// 광고 리소스 삭제
		public static class Delete
		{
			public class Request
			{
				public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AdResourceId.IsValid() == false)
						return false;

					return true;
				}
			}
		}

		// 광고 리소스 정보
		public static class Info
		{
			public class Request
			{
				public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AdResourceId.IsValid() == false)
						return false;

					return true;
				}
			}

			public class Response : AdResourceData
			{
			}
		}

		// 광고 리소스 조회
		public static class List
		{
			public class Response
			{
				public AdResourceData[] AdResources { get; set; }
			}
		}
	}
}