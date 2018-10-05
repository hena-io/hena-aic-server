using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 앱 데이터
	public class AppData : IJSONSerializable, ICloneable<AppData>
	{
		// 기본값
		public readonly static AppData Default = new AppData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 앱 DBKey
		public DBKey AppDBKey = GlobalDefine.INVALID_DBKEY;

		// 앱 이름( Max 80 )
		public string Name = string.Empty;

		// 앱 마켓 타입
		public AppMarketTypes AppMarketType = AppMarketTypes.None;

		// 삭제상태 체크
		public bool IsDeleted = false;

		// 삭제된 시간
		public DateTime DeletedTime = DateTime.MinValue;

		// 생성된 시간
		public DateTime CreateTime = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate = DateTime.MinValue;
		
		#region ICloneable
		public AppData Clone()
		{
			return this.Clone<AppData>();
		}

		public void CopyTo(ref AppData target)
		{
			target.UserDBKey = UserDBKey;
			target.AppDBKey = AppDBKey;
			target.Name = Name;
			target.AppMarketType = AppMarketType;
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
			AppDBKey = JSONUtility.GetValue(token, "CampaignDBKey", Default.AppDBKey);
			Name = JSONUtility.GetValue(token, "Name", Default.Name);
			AppMarketType = JSONUtility.GetValue(token, "AppMarketType", Default.AppMarketType);
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
			jObject["CampaignDBKey"] = AppDBKey;
			jObject["Name"] = Name;
			jObject["AppMarketType"] = AppMarketType.ToString();
			jObject["IsDeleted"] = IsDeleted;
			jObject["DeletedTime"] = DeletedTime;
			jObject["CreateTime"] = CreateTime;
			jObject["LastUpdate"] = LastUpdate;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
