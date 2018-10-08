//using Hena;
//using Hena.Shared.Data;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HenaWebsite.Models.API.AdDesign
//{
//	public static class AdDesignModels
//	{
//		// 광고 디자인 생성
//		public static class Create
//		{
//			public class Request
//			{
//				[JsonProperty(PropertyName = "name")]
//				public string Name { get; set; } = string.Empty;

//				[JsonProperty(PropertyName = "type")]
//				[JsonConverter(typeof(StringEnumConverter))]
//				public CampaignTypes Type { get; set; } = CampaignTypes.None;

//				[JsonProperty(PropertyName = "cost")]
//				public decimal Cost { get; set; } = 0m;

//				[JsonProperty(PropertyName = "targetValue")]
//				public long TargetValue { get; set; } = 0;

//				[JsonProperty(PropertyName = "beginDate")]
//				public DateTime BeginTime { get; set; } = DateTime.MinValue;

//				[JsonProperty(PropertyName = "endDate")]
//				public DateTime EndTime { get; set; } = DateTime.MinValue;

//				//public virtual void Fill(AdDesignData target)
//				//{
//				//	target.Name = Name.Trim();
//				//	target.CampaignType = Type;
//				//	target.Cost = Cost;
//				//	target.TargetValue = TargetValue;
//				//	target.BeginTime = BeginTime;
//				//	target.EndTime = EndTime;
//				//}

//				public virtual AdDesignData ToAdDesignData(DBKey userId, DBKey campaignId)
//				{
//					var item = new AdDesignData();
//					//Fill(item);
//					item.UserId = userId;
//					item.CampaignId = campaignId;
//					return item;
//				}

//				public virtual bool IsValidParameters()
//				{
//					var name = Name.Trim();
//					if (string.IsNullOrEmpty(name) || name.Length < 2)
//						return false;

//					return true;
//				}
//			}

//			public class Response
//			{
//				[JsonProperty(PropertyName = "id")]
//				public string Id { get; set; }

//				[JsonProperty(PropertyName = "name")]
//				public string Name = string.Empty;

//				[JsonProperty(PropertyName = "type")]
//				[JsonConverter(typeof(StringEnumConverter))]
//				public CampaignTypes Type = CampaignTypes.None;

//				[JsonProperty(PropertyName = "targetValue")]
//				public long TargetValue = 0;

//				[JsonProperty(PropertyName = "cost")]
//				public decimal Cost = 0m;

//				[JsonProperty(PropertyName = "beginDate")]
//				public DateTime BeginTime = DateTime.MinValue;

//				[JsonProperty(PropertyName = "endDate")]
//				public DateTime EndTime = DateTime.MinValue;

//				[JsonProperty(PropertyName = "isPause")]
//				public bool IsPause = false;

//				[JsonProperty(PropertyName = "createdAt")]
//				public DateTime CreateTime = DateTime.MinValue;

//				[JsonProperty(PropertyName = "updatedAt")]
//				public DateTime LastUpdate = DateTime.MinValue;

//				public void From(AdDesignData item)
//				{
//					Id = item.CampaignId.ToString();
//					Name = item.Name;
//					Type = item.CampaignType;
//					TargetValue = item.TargetValue;
//					Cost = item.Cost;
//					BeginTime = item.BeginTime;
//					EndTime = item.EndTime;
//					IsPause = item.IsPause;
//					CreateTime = item.CreateTime;
//					LastUpdate = item.LastUpdate;
//				}
//			}
//		}

//		// 광고 디자인 수정
//		public static class Modify
//		{
//			public class Request : Create.Request
//			{
//				[JsonProperty(PropertyName = "id")]
//				public string Id { get; set; } = string.Empty;

//				public override bool IsValidParameters()
//				{
//					if (string.IsNullOrEmpty(Id) || Id.ToLong() <= 0)
//						return false;

//					return base.IsValidParameters();
//				}
//			}

//			public class Response : Create.Response
//			{
//			}
//		}

//		// 광고 디자인 삭제
//		public static class Delete
//		{
//			public class Request
//			{
//				[JsonProperty(PropertyName = "id")]
//				public string Id { get; set; } = string.Empty;

//				public virtual bool IsValidParameters()
//				{
//					if (string.IsNullOrEmpty(Id) || Id.ToLong() <= 0)
//						return false;

//					return true;
//				}
//			}

//			public class Response
//			{
//			}
//		}

//	}
//}