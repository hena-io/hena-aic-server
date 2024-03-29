﻿using Hena;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models.API.AdDesign
{
	public static class AdDesignModels
	{
		// 광고 디자인 생성
		public static class Create
		{
			public class Request
			{
				[Required]
				public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;
				[Required]
				public string Name { get; set; } = string.Empty;
				[Required]
				public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;
				[Required]
				public string DestinationUrl { get; set; } = string.Empty;

				public virtual bool IsValidParameters()
				{
					if (DBKey.CheckValidations(CampaignId, AdResourceId) == false)
						return false;

					if (string.IsNullOrEmpty(Name) || Name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : AdDesignData
			{
			}
		}

		// 광고 디자인 수정
		public static class Modify
		{
			public class Request
			{
				public DBKey AdDesignId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public string Name { get; set; } = string.Empty;
				public DBKey AdResourceId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public string DestinationUrl { get; set; } = string.Empty;

				public virtual bool IsValidParameters()
				{
					if (DBKey.CheckValidations(AdDesignId, AdResourceId) == false)
						return false;

					if (string.IsNullOrEmpty(Name) || Name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : AdDesignData
			{
			}
		}

		// 광고 디자인 삭제
		public static class Delete
		{
			public class Request
			{
				public DBKey AdDesignId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (AdDesignId.IsValid() == false)
						return false;

					return true;
				}
			}
		}

		// 광고 디자인 목록
		public static class List
		{
			public class Request
			{
				public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (CampaignId.IsValid() == false)
						return false;

					return true;
				}
			}

			public class Response
			{
				public AdDesignData[] AdDesigns { get; set; }
			}
		}
	}
}