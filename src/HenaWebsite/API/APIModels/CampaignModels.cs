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

namespace HenaWebsite.Models.API.Campaign
{
	[Serializable]
	public class CampaignModels
	{
		// 캠페인 생성
		public static class Create
		{
			public class Request
			{
				public string Name { get; set; } = string.Empty;
				public CampaignTypes CampaignType { get; set; } = CampaignTypes.None;
				public decimal Cost { get; set; } = 0m;
				public long TargetValue { get; set; } = 0;
				public DateTime BeginTime { get; set; } = DateTime.MinValue;
				public DateTime EndTime { get; set; } = DateTime.MinValue;

				public virtual bool IsValidParameters()
				{
					var name = Name.Trim();
					if (string.IsNullOrEmpty(name) || name.Length < 2)
						return false;

					return true;
				}
			}

			public class Response : CampaignData
			{
			}
		}

		// 캠페인 수정
		public static class Modify
		{
			public class Request : Create.Request
			{
				public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public override bool IsValidParameters()
				{
					if (CampaignId.IsValid() == false)
						return false;

					return base.IsValidParameters();
				}
			}

			public class Response : Create.Response
			{
			}
		}

		// 캠페인 삭제
		public static class Delete
		{
			public class Request
			{
				public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;

				public virtual bool IsValidParameters()
				{
					if (CampaignId <= 0)
						return false;

					return true;
				}
			}
		}

		// 캠페인 목록
		public static class List
		{
			public class Response
			{
				public CampaignData[] Campaigns { get; set; }
			}
		}

	}
}