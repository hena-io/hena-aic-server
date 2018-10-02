using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 캠페인 디자인 데이터
	public class CampaignDesignData : IJSONSerializable, ICloneable<CampaignDesignData>
	{
		// 기본값
		public readonly static CampaignDesignData Default = new CampaignDesignData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 캠페인 DBKey
		public DBKey CampaignDBKey = GlobalDefine.INVALID_DBKEY;

		// 캠페인 디자인 DBKey
		public DBKey DesignDBKey = GlobalDefine.INVALID_DBKEY;

		// 디자인 이름
		public string DesignName = string.Empty;

		// 디자인 타입
		public CampaignDesignTypes DesignType = CampaignDesignTypes.None;

		// 업로드된 리소스 이름
		public string ResourceName = string.Empty;

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
		public CampaignDesignData Clone()
		{
			return this.Clone<CampaignDesignData>();
		}

		public void CopyTo(ref CampaignDesignData target)
		{
			target.UserDBKey = UserDBKey;
			target.CampaignDBKey = CampaignDBKey;
			target.DesignDBKey = DesignDBKey;
			target.DesignName = DesignName;
			target.DesignType = DesignType;
			target.ResourceName = ResourceName;
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
			DesignDBKey = JSONUtility.GetValue(token, "DesignDBKey", Default.DesignDBKey);
			DesignName = JSONUtility.GetValue(token, "DesignName", Default.DesignName);
			DesignType = JSONUtility.GetValue(token, "DesignType", Default.DesignType);
			ResourceName = JSONUtility.GetValue(token, "ResourceName", Default.ResourceName);
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
			jObject["DesignDBKey"] = DesignDBKey;
			jObject["DesignName"] = DesignName;
			jObject["DesignType"] = DesignType.ToString();
			jObject["ResourceName"] = ResourceName;
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
