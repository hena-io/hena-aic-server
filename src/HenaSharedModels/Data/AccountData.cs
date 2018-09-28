using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 전체 데이터
	public class AccountData : IJSONSerializable, ICloneable<AccountData>
	{
		// 기본값
		private readonly static AccountData Default = new AccountData();

		// 계정 기본정보
		public AccountBasicData BasicData = new AccountBasicData();

		// 계정 권한
		public AccountPermissionDataContainer Permissions = new AccountPermissionDataContainer();

        // 유저 타임존 Offset
        public TimeSpan TimeZoneOffset = TimeSpan.Zero;

        public object CustomData { get; set; }

		#region ICloneable
		public AccountData Clone()
		{
			return this.Clone<AccountData>();
		}

		public void CopyTo(ref AccountData target)
		{
            BasicData.CopyTo(ref target.BasicData);
			Permissions.CopyTo(ref target.Permissions);
            target.TimeZoneOffset = TimeZoneOffset;
        }
        #endregion // ICloneable

        #region IJSONSerializable
        public bool FromJSON(JToken token)
		{
            if (BasicData.FromJSON(token["BasicData"]) == false)
				return false;

			if (Permissions.FromJSON(token["Permissions"]) == false)
				return false;

            TimeZoneOffset = JSONUtility.GetValue(token, "TimeZoneOffset", Default.TimeZoneOffset);

			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["BasicData"] = BasicData.ToJSON();
			jObject["Permissions"] = Permissions.ToJSON();
            jObject["TimeZoneOffset"] = TimeZoneOffset;
            return jObject;
		}
		#endregion // IJSONSerializable
	}
}
