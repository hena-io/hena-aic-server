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
	public class UserBasicData
	{
		// 기본값
		public readonly static UserBasicData Default = new UserBasicData();

		// 유저 Id
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
		
		// 이메일
		public string EMail { get; set; } = string.Empty;

		// 비밀번호
		public string Password { get; set; } = string.Empty;

		// 언어
		public string Language { get; set; } = string.Empty;

		// 타임존 ID
		public string TimeZoneId { get; set; } = string.Empty;

		// 이름
		public string GivenName { get; set; } = string.Empty;

		// 성
		public string SurName { get; set; } = string.Empty;

        // 지역코드
        public string RegionCodeForNumber { get; set; } = "";

        // 국가번호
        public int CountryCode { get; set; } = 00;

        // 전화번호
        public long NationalNumber { get; set; } = 0100000000;

		// 계정 삭제여부
		public bool IsDeleted { get; set; } = false;

		// 계정 삭제 시간
		public DateTime DeletedTime { get; set; } = GlobalDefine.INVALID_DATETIME;

		// 계정 생성 시간
		public DateTime CreateTime { get; set; } = GlobalDefine.INVALID_DATETIME;

		// DB 마지막 업데이트 시간
		public DateTime LastUpdate { get; set; } = GlobalDefine.INVALID_DATETIME;


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
	}
}
