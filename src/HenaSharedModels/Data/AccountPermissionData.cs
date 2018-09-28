using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 권한
	public class AccountPermissionData : IJSONSerializable, ICloneable<AccountPermissionData>
	{
		// 기본값
		private readonly static AccountPermissionData Default = new AccountPermissionData();

		// 계정 DB키
		public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;

		// 권한 타입
		public AccountPermissionType PermissionType = AccountPermissionType.None;

		// 권한 레벨
		public short Level = 0;

		// 권한 등록일
		public DateTime RegisterTime = GlobalDefine.INVALID_DATETIME;

		public bool CheckLevel(short level = 0)
		{
			return Level >= level;
		}

	
		#region ICloneable
		public AccountPermissionData Clone()
		{
			return this.Clone<AccountPermissionData>();
		}

		public void CopyTo(ref AccountPermissionData target)
		{
			target.AccountDBKey = AccountDBKey;
			target.PermissionType = PermissionType;
			target.Level = Level;
			target.RegisterTime = RegisterTime;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			AccountDBKey = JSONUtility.GetValue(token, "AccountDBKey", Default.AccountDBKey);
			PermissionType = JSONUtility.GetValueEnum(token, "PermissionType", Default.PermissionType);
			Level = JSONUtility.GetValue(token, "Level", Default.Level);
			RegisterTime = JSONUtility.GetValue(token, "RegisterTime", Default.RegisterTime);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["AccountDBKey"] = AccountDBKey;
			jObject["PermissionType"] = PermissionType.ToString();
			jObject["Level"] = Level;
			jObject["RegisterTime"] = RegisterTime;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
