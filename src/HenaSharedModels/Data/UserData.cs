using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 전체 데이터
	public class UserData : IJSONSerializable, ICloneable<UserData>
	{
		// 기본값
		private readonly static UserData Default = new UserData();

		// 계정 기본정보
		public UserBasicData BasicData = new UserBasicData();

		// 계정 권한
		public UserPermissionDataContainer Permissions = new UserPermissionDataContainer();

        // 유저 타임존 Offset
        public TimeSpan TimeZoneOffset = TimeSpan.Zero;

        public object CustomData { get; set; }

		#region ICloneable
		public UserData Clone()
		{
			return this.Clone<UserData>();
		}

		public void CopyTo(ref UserData target)
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
