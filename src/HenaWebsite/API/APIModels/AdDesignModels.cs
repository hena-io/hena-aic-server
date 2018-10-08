using Hena;
using Hena.Shared.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
				public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public DBKey CampaignId { get; set; } = GlobalDefine.INVALID_DBKEY;
				public string Name { get; set; } = string.Empty;
				public AdDesignTypes AdDesignType { get; set; } = AdDesignTypes.None;
				public string ResourceName { get; set; } = string.Empty;
				public string DestinationUrl { get; set; } = string.Empty;

				public virtual void Fill(AdDesignData target)
				{
					target.UserId = UserId;
					target.CampaignId = CampaignId;
					target.Name = Name;
					target.AdDesignType = AdDesignType;
					target.ResourceName = ResourceName;
					target.DestinationUrl = DestinationUrl;
				}

				public virtual AdDesignData ToAdDesignData(DBKey adDesignId)
				{
					var item = new AdDesignData();
					Fill(item);
					item.AdDesignId = adDesignId;
					return item;
				}

				public virtual bool IsValidParameters()
				{
					//var name = AdDesignName.Trim();
					//if (string.IsNullOrEmpty(name) || name.Length < 2)
					//	return false;

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
			public class Request : Create.Request
			{
				[JsonProperty(PropertyName = "id")]
				public string Id { get; set; } = string.Empty;

				public override bool IsValidParameters()
				{
					if (string.IsNullOrEmpty(Id) || Id.ToLong() <= 0)
						return false;

					return base.IsValidParameters();
				}
			}

			public class Response : Create.Response
			{
			}
		}

		// 광고 디자인 삭제
		public static class Delete
		{
			public class Request
			{
				public string Id { get; set; } = string.Empty;

				public virtual bool IsValidParameters()
				{
					if (string.IsNullOrEmpty(Id) || Id.ToLong() <= 0)
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