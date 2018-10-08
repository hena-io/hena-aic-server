using Hena;
using Hena.Library.Attributes;
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
	public static class CampaignModels
	{
		// 캠페인 생성
		public static class Create
		{
			[Serializable]
			[JsonConverter(typeof(TrimConverter<Request>))]
			public class Request
			{
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				[Trim]
				public string Name { get; set; } = string.Empty;
				[JsonConverter(typeof(StringEnumConverter))]
				public CampaignTypes CampaignType { get; set; } = CampaignTypes.None;
				public decimal Cost { get; set; } = 0m;
				public long TargetValue { get; set; } = 0;
				public DateTime BeginTime { get; set; } = DateTime.MinValue;
				public DateTime EndTime { get; set; } = DateTime.MinValue;

				public virtual void Fill(CampaignData target)
				{
					target.UserId = UserId;
					target.Name = Name;
					target.CampaignType = CampaignType;
					target.Cost = Cost;
					target.TargetValue = TargetValue;
					target.BeginTime = BeginTime;
					target.EndTime = EndTime;
				}

				public virtual CampaignData ToCampaignData(DBKey userId, DBKey campaignId)
				{
					var item = new CampaignData();
					Fill(item);
					item.UserId = userId;
					item.CampaignId = campaignId;
					return item;
				}

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
				public DBKey CampaignId { get; set; } = string.Empty;

				public override bool IsValidParameters()
				{
					if (CampaignId <= 0)
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

			public class Response
			{
			}
		}

	}
}