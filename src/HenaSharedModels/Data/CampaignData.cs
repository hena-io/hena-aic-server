using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 캠페인 데이터
	public class CampaignData : IJSONSerializable, ICloneable<CampaignData>
	{
		// 기본값
		public readonly static CampaignData Default = new CampaignData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 캠페인 DBKey
		public DBKey CampaignDBKey = GlobalDefine.INVALID_DBKEY;

		// 캠페인 이름( Max 80 )
		public string Name = string.Empty;

		// 캠페인 타입
		public CampaignTypes CampaignType = CampaignTypes.None;

		// 캠표인 목표( CPC -> 목표 클릭 수, CPM -> 노출 목표 수 )
		public long TargetValue = 0;

		// 캠페인 비용( CPC -> 클릭당 비용, CPM -> 1000노출당 비용 )
		public decimal Cost = 0m;

		// 캠페인 시작 시간
		public DateTime BeginTime = DateTime.MinValue;

		// 캠페인 만료 시간
		public DateTime EndTime = DateTime.MinValue;

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
		public CampaignData Clone()
		{
			return this.Clone<CampaignData>();
		}

		public void CopyTo(ref CampaignData target)
		{
			target.UserDBKey = UserDBKey;
			target.CampaignDBKey = CampaignDBKey;
			target.Name = Name;
			target.CampaignType = CampaignType;
			target.TargetValue = TargetValue;
			target.Cost = Cost;
			target.BeginTime = BeginTime;
			target.EndTime = EndTime;
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
			Name = JSONUtility.GetValue(token, "Name", Default.Name);
			CampaignType = JSONUtility.GetValue(token, "CampaignType", Default.CampaignType);
			TargetValue = JSONUtility.GetValue(token, "TargetValue", Default.TargetValue);
			Cost = JSONUtility.GetValue(token, "Cost", Default.Cost);
			BeginTime = JSONUtility.GetValue(token, "BeginTime", Default.BeginTime);
			EndTime = JSONUtility.GetValue(token, "EndTime", Default.EndTime);
			IsPause = JSONUtility.GetValue(token, "IsPause", Default.IsPause);
			IsDeleted = JSONUtility.GetValue(token, "IsDeleted", Default.IsDeleted);
			DeletedTime = JSONUtility.GetValue(token, "DeletedTime", Default.DeletedTime);
			CreateTime = JSONUtility.GetValue(token, "CreateTime", Default.CreateTime);
			LastUpdate = JSONUtility.GetValue(token, "LastUpdate", Default.LastUpdate);
			return true;
		}
		public JToken ToJSON()
		{
			return ToJSON(SerializationMode.Normal);
		}

		public JToken ToJSON(SerializationMode serializationMode)
		{
			var jObject = new JObject();
			jObject["UserDBKey"] = UserDBKey;
			jObject["CampaignDBKey"] = CampaignDBKey;
			jObject["Name"] = Name;
			jObject["CampaignType"] = CampaignType.ToString();
			jObject["TargetValue"] = TargetValue;
			jObject["Cost"] = Cost;
			jObject["BeginTime"] = BeginTime;
			jObject["EndTime"] = EndTime;
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
