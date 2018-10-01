using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 기본 정보
	public class UserBasicData : IJSONSerializable, ICloneable<UserBasicData>
	{
		// 기본값
		public readonly static UserBasicData Default = new UserBasicData();

		// 유저 DBKey
		public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;
		
		// 이메일
		public string EMail = string.Empty;

		// 비밀번호
		public string Password = string.Empty;

		// 언어
		public string Language = string.Empty;

		// 타임존 ID
		public string TimeZoneId = string.Empty;

		// 이름
		public string GivenName = string.Empty;

		// 성
		public string SurName = string.Empty;

        // 지역코드
        public string RegionCodeForNumber = "";

        // 국가번호
        public int CountryCode = 00;

        // 전화번호
        public long NationalNumber = 0100000000;

		// 계정 삭제여부
		public bool IsDeleted = false;

		// 계정 삭제 시간
		public DateTime DeletedTime = GlobalDefine.INVALID_DATETIME;

		// 계정 생성 시간
		public DateTime CreateTime = GlobalDefine.INVALID_DATETIME;

		// DB 마지막 업데이트 시간
		public DateTime LastUpdate = GlobalDefine.INVALID_DATETIME;


		// 전화번호
		public string PhoneNumber
        {
            get { return GeneratePhoneNumberString(CountryCode, NationalNumber); }
        }

        // 전화번호 문자열 생성
        public static string GeneratePhoneNumberString(int countryCode, long nationalNumber)
        {
            return $"+{countryCode} {nationalNumber}";
        }

        public static string GeneratePhoneNumberString(string countryCode, string nationalNumber)
        {
            return $"+{countryCode} {nationalNumber}";
        }

        // 사용 가능한 패스워드인지 체크
        public static bool CheckPassword(string input, int minMatchCount = 3)
        {
			if (input.Length < 8 || input.Length >= 32)
            {
				return false;
            }

			var hasNumber = new Regex(@"[0-9]+");
			var hasUpperChar = new Regex(@"[A-Z]+");
			var hasLowerChar = new Regex(@"[a-z]+");
			var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

			int matchCount = 0;

			if (hasLowerChar.IsMatch(input))
				++matchCount;

			if (hasUpperChar.IsMatch(input))
				++matchCount;

			if (hasNumber.IsMatch(input))
				++matchCount;

			if (hasSymbols.IsMatch(input))
				++matchCount;

			return matchCount >= minMatchCount;
        }

		#region ICloneable
		public UserBasicData Clone()
		{
			return this.Clone<UserBasicData>();
		}

		public void CopyTo(ref UserBasicData target)
		{
			target.UserDBKey = UserDBKey;
			target.EMail = EMail;
			target.Password = Password;
			target.Language = Language;
			target.TimeZoneId = TimeZoneId;
			target.GivenName = GivenName;
			target.SurName = SurName;
			target.RegionCodeForNumber = RegionCodeForNumber;
			target.CountryCode = CountryCode;
			target.NationalNumber = NationalNumber;
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
			EMail = JSONUtility.GetValue(token, "EMail", Default.EMail);
			Password = JSONUtility.GetValue(token, "Password", Default.Password);
			Language = JSONUtility.GetValue(token, "Language", Default.Language);
			TimeZoneId = JSONUtility.GetValue(token, "TimeZoneId", Default.TimeZoneId);
			GivenName = JSONUtility.GetValue(token, "GivenName", Default.GivenName);
			SurName = JSONUtility.GetValue(token, "SurName", Default.SurName);
			RegionCodeForNumber = JSONUtility.GetValue(token, "RegionCodeForNumber", Default.RegionCodeForNumber);
			CountryCode = JSONUtility.GetValue(token, "CountryCode", Default.CountryCode);
			NationalNumber = JSONUtility.GetValue(token, "NationalNumber", Default.NationalNumber);
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
			jObject["EMail"] = EMail;
			jObject["Password"] = Password;
			jObject["Language"] = Language;
			jObject["TimeZoneId"] = TimeZoneId;
			jObject["GivenName"] = GivenName;
			jObject["SurName"] = SurName;
			jObject["RegionCodeForNumber"] = RegionCodeForNumber;
			jObject["CountryCode"] = CountryCode;
			jObject["NationalNumber"] = NationalNumber;
			jObject["IsDeleted"] = IsDeleted;
			jObject["DeletedTime"] = DeletedTime;
			jObject["CreateTime"] = CreateTime;
			jObject["LastUpdate"] = LastUpdate;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
