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
	public class AccountBasicData : IJSONSerializable, ICloneable<AccountBasicData>
	{
		// 기본값
		private readonly static AccountBasicData Default = new AccountBasicData();

		// 계정 DB키
		public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;

		// 계정 생성 시간
		public DateTime CreateTime = GlobalDefine.INVALID_DATETIME;

		// 이름
		public string GivenName = string.Empty;

		// 성
		public string SurName = string.Empty;

		// 유저명( ID )
		public string Username = string.Empty;

		// 이메일
		public string EMail = string.Empty;

		// 비밀번호
		public string Password = string.Empty;

        // 지역코드
        public string RegionCodeForNumber = "KR";

        // 국가번호
        public int CountryCode = 82;

        // 전화번호
        public long NationalNumber = 0100000000;

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
		public AccountBasicData Clone()
		{
			return this.Clone<AccountBasicData>();
		}

		public void CopyTo(ref AccountBasicData target)
		{
			target.AccountDBKey = AccountDBKey;
			target.CreateTime = CreateTime;
			target.GivenName = GivenName;
			target.SurName = SurName;
			target.Username = Username;
			target.EMail = EMail;
			target.Password = Password;
            target.RegionCodeForNumber = RegionCodeForNumber;
			target.CountryCode = CountryCode;
            target.NationalNumber = NationalNumber;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			AccountDBKey = JSONUtility.GetValue(token, "AccountDBKey", Default.AccountDBKey);
			CreateTime = JSONUtility.GetValue(token, "CreateTime", Default.CreateTime);
			GivenName = JSONUtility.GetValue(token, "GivenName", Default.GivenName);
			SurName = JSONUtility.GetValue(token, "SurName", Default.SurName);
			Username = JSONUtility.GetValue(token, "Username", Default.Username);
			EMail = JSONUtility.GetValue(token, "EMail", Default.EMail);
			Password = JSONUtility.GetValue(token, "Password", Default.Password);
			RegionCodeForNumber = JSONUtility.GetValue(token, "RegionCodeForNumber", Default.RegionCodeForNumber);
			CountryCode = JSONUtility.GetValue(token, "CountryCode", Default.CountryCode);
			NationalNumber = JSONUtility.GetValue(token, "NationalNumber", Default.NationalNumber);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["AccountDBKey"] = AccountDBKey;
			jObject["GivenName"] = GivenName;
			jObject["SurName"] = SurName;
			jObject["Username"] = Username;
			jObject["EMail"] = EMail;
			jObject["Password"] = Password;
			jObject["RegionCodeForNumber"] = RegionCodeForNumber;
			jObject["CountryCode"] = CountryCode;
			jObject["NationalNumber"] = NationalNumber;
			jObject["CreateTime"] = CreateTime;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
