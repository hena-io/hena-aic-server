using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 권한
	public class UserPermissionData : IJSONSerializable, ICloneable<UserPermissionData>
	{
		// 기본값
		private readonly static UserPermissionData Default = new UserPermissionData();

		// 계정 DB키
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

		// 권한 타입
		public UserPermissionTypes PermissionType = UserPermissionTypes.None;

		// 권한 레벨
		public short Level = 0;

		// 권한 등록일
		public DateTime RegisterTime = GlobalDefine.INVALID_DATETIME;

		public bool CheckLevel(short level = 0)
		{
			return Level >= level;
		}

	
		#region ICloneable
		public UserPermissionData Clone()
		{
			return this.Clone<UserPermissionData>();
		}

		public void CopyTo(ref UserPermissionData target)
		{
			target.UserDBKey = UserDBKey;
			target.PermissionType = PermissionType;
			target.Level = Level;
			target.RegisterTime = RegisterTime;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			UserDBKey = JSONUtility.GetValue(token, "UserDBKey", Default.UserDBKey);
			PermissionType = JSONUtility.GetValueEnum(token, "PermissionType", Default.PermissionType);
			Level = JSONUtility.GetValue(token, "Level", Default.Level);
			RegisterTime = JSONUtility.GetValue(token, "RegisterTime", Default.RegisterTime);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["UserDBKey"] = UserDBKey;
			jObject["PermissionType"] = PermissionType.ToString();
			jObject["Level"] = Level;
			jObject["RegisterTime"] = RegisterTime;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
