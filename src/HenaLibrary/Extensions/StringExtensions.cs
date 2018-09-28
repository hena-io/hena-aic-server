using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hena
{
    public static class StringExtensions
    {
		public static bool TrySubStringByKeyword(this string item, string beginKeyword, string endKeyword, out string result)
		{
			int beginPosition, endPosition;
			return TrySubStringByKeyword(item, beginKeyword, endKeyword, out result, out beginPosition, out endPosition);
		}

		public static bool TrySubStringByKeyword(this string item, string beginKeyword, string endKeyword, out string result, out int beginPosition, out int endPosition)
		{
			beginPosition = -1;
			endPosition = -1;
			result = string.Empty;
			try
			{
				beginPosition = item.IndexOf(beginKeyword);
				if (beginPosition == -1)
					return false;

				endPosition = item.IndexOf(endKeyword, beginPosition + beginKeyword.Length);
				if (endPosition == -1)
					return false;

				int subStringBegin = beginPosition + beginKeyword.Length;
				result = item.Substring(subStringBegin, endPosition - subStringBegin);
				return true;
			}
			catch (Exception) { return false; }
		}

		public static bool ToBool(this string item, bool defaultValue = false)
        {
            bool value = defaultValue;
            if (bool.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static int ToInt(this string item, int defaultValue = 0)
        {
            int value = defaultValue;
            if (int.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static long ToLong(this string item, long defaultValue = 0)
        {
            long value = defaultValue;
            if (long.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static float ToFloat(this string item, float defaultValue = 0)
        {
            float value = defaultValue;
            if (float.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static double ToDouble(this string item, double defaultValue = 0)
        {
            double value = defaultValue;
            if (double.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static DateTime ToDateTime(this string item, DateTime defaultValue = default(DateTime))
        {
            DateTime value = defaultValue;
            if (DateTime.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static TimeSpan ToTimeSpan(this string item, TimeSpan defaultValue = default(TimeSpan))
        {
            TimeSpan value = defaultValue;
            if (TimeSpan.TryParse(item, out value)) { return value; }
            return defaultValue;
        }

        public static TEnum ToEnum<TEnum>(this string item, bool ignoreCase = true, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            TEnum value;
            if (Enum.TryParse(item, ignoreCase, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static bool ToEnum<TEnum>(this string item, out TEnum outValue, bool ignoreCase = true) where TEnum : struct
        {
            return Enum.TryParse(item, ignoreCase, out outValue);
        }

		// 이메일 포멧 체크
		static Regex Regex_IsValidEmailAddress = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
		public static bool IsValidEmailAddress(this string s)
		{
			return Regex_IsValidEmailAddress.IsMatch(s);
		}

		// 유저명 포멧 체크
		static Regex Regex_IsValidUsername = new Regex(@"^(?=.{3,20}$)([A-Za-z0-9][._-]?)*$");
		public static bool IsValidUsername(this string s)
		{
			return Regex_IsValidUsername.IsMatch(s);
		}
	}
}
