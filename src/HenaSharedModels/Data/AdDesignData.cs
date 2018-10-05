using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 광고 디자인 데이터
	public class AdDesignData : IJSONSerializable, ICloneable<AdDesignData>
	{
		// 기본값
		public readonly static AdDesignData Default = new AdDesignData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 캠페인 DBKey
		public DBKey CampaignDBKey = GlobalDefine.INVALID_DBKEY;

		// 광고 디자인 DBKey
		public DBKey AdDesignDBKey = GlobalDefine.INVALID_DBKEY;

		// 디자인 이름( Max 80 )
		public string Name = string.Empty;

		// 광고 디자인 타입
		public AdDesignTypes AdDesignType = AdDesignTypes.None;

		// 업로드된 리소스 이름
		public string ResourceName = string.Empty;

		// 목적지 URL
		public string DestinationUrl = string.Empty;

		// 캠페인 일시정지 상태
		public bool IsPause = false;

		// 삭제상태 체크
		public bool IsDeleted = false;

		// 삭제된 시간
		public DateTime DeletedTime = DateTime.MinValue;

		// 생성된 시간
		public DateTime CreateTime = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate = DateTime.MinValue;
		
		#region ICloneable
		public AdDesignData Clone()
		{
			return this.Clone<AdDesignData>();
		}

		public void CopyTo(ref AdDesignData target)
		{
			target.UserDBKey = UserDBKey;
			target.CampaignDBKey = CampaignDBKey;
			target.AdDesignDBKey = AdDesignDBKey;
			target.Name = Name;
			target.AdDesignType = AdDesignType;
			target.ResourceName = ResourceName;
			target.DestinationUrl = DestinationUrl;
			target.IsPause = IsPause;
			target.IsDeleted = IsDeleted;
			target.DeletedTime = DeletedTime;
			target.CreateTime = CreateTime;
			target.LastUpdate = LastUpdate;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			UserDBKey = JSONUtility.GetValue(token, "UserDBKey", Default.UserDBKey);
			CampaignDBKey = JSONUtility.GetValue(token, "CampaignDBKey", Default.CampaignDBKey);
			AdDesignDBKey = JSONUtility.GetValue(token, "AdDesignDBKey", Default.AdDesignDBKey);
			Name = JSONUtility.GetValue(token, "Name", Default.Name);
			AdDesignType = JSONUtility.GetValue(token, "AdDesignType", Default.AdDesignType);
			ResourceName = JSONUtility.GetValue(token, "ResourceName", Default.ResourceName);
			DestinationUrl = JSONUtility.GetValue(token, "DestinationUrl", Default.DestinationUrl);
			IsPause = JSONUtility.GetValue(token, "IsPause", Default.IsPause);
			IsDeleted = JSONUtility.GetValue(token, "IsDeleted", Default.IsDeleted);
			DeletedTime = JSONUtility.GetValue(token, "DeletedTime", Default.DeletedTime);
			CreateTime = JSONUtility.GetValue(token, "CreateTime", Default.CreateTime);
			LastUpdate = JSONUtility.GetValue(token, "LastUpdate", Default.LastUpdate);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["UserDBKey"] = UserDBKey;
			jObject["CampaignDBKey"] = CampaignDBKey;
			jObject["AdDesignDBKey"] = AdDesignDBKey;
			jObject["Name"] = Name;
			jObject["AdDesignType"] = AdDesignType.ToString();
			jObject["ResourceName"] = ResourceName;
			jObject["DestinationUrl"] = DestinationUrl;
			jObject["IsPause"] = IsPause;
			jObject["IsDeleted"] = IsDeleted;
			jObject["DeletedTime"] = DeletedTime;
			jObject["CreateTime"] = CreateTime;
			jObject["LastUpdate"] = LastUpdate;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
