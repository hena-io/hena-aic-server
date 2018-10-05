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
	public class AdUnitData : IJSONSerializable, ICloneable<AdUnitData>
	{
		// 기본값
		public readonly static AdUnitData Default = new AdUnitData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 앱 DBKey
		public DBKey AppDBKey = GlobalDefine.INVALID_DBKEY;
		
		// 광고유닛 DBKey
		public DBKey AdUnitDBKey = GlobalDefine.INVALID_DBKEY;

		// 광고유닛 이름( Max 80 )
		public string Name = string.Empty;

		// 광고 디자인 타입
		public AdDesignTypes AdDesignType = AdDesignTypes.None;

		// 생성된 시간
		public DateTime CreateTime = DateTime.MinValue;

		// 마지막 업데이트된 시간
		public DateTime LastUpdate = DateTime.MinValue;
		
		#region ICloneable
		public AdUnitData Clone()
		{
			return this.Clone<AdUnitData>();
		}

		public void CopyTo(ref AdUnitData target)
		{
			target.UserDBKey = UserDBKey;
			target.AppDBKey = AppDBKey;
			target.Name = Name;
			target.AdDesignType = AdDesignType;
			target.CreateTime = CreateTime;
			target.LastUpdate = LastUpdate;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			UserDBKey = JSONUtility.GetValue(token, "UserDBKey", Default.UserDBKey);
			AppDBKey = JSONUtility.GetValue(token, "AppDBKey", Default.AppDBKey);
			Name = JSONUtility.GetValue(token, "Name", Default.Name);
			AdDesignType = JSONUtility.GetValue(token, "AdDesignType", Default.AdDesignType);
			CreateTime = JSONUtility.GetValue(token, "CreateTime", Default.CreateTime);
			LastUpdate = JSONUtility.GetValue(token, "LastUpdate", Default.LastUpdate);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["UserDBKey"] = UserDBKey;
			jObject["AppDBKey"] = AppDBKey;
			jObject["Name"] = Name;
			jObject["AdDesignType"] = AdDesignType.ToString();
			jObject["CreateTime"] = CreateTime;
			jObject["LastUpdate"] = LastUpdate;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
