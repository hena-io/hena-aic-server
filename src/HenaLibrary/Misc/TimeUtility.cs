using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hena
{
	public static class TimeUtility
	{

		#region UTC Time Offset
		private static Dictionary<string, TimeSpan> _utcTimeOffsets;

		private static void AddUtcTimeOffset(string id, string displayName, TimeSpan offset)
		{
			_utcTimeOffsets.Add(id, offset);
		}

		private static void InitUtcTimeOffset()
		{
			if (_utcTimeOffsets != null)
				return;

			 _utcTimeOffsets = new Dictionary<string, TimeSpan>(StringComparer.CurrentCultureIgnoreCase);

			AddUtcTimeOffset("Dateline Standard Time", "날짜 변경선 표준시", TimeSpan.Parse("-12:00:00"));
			AddUtcTimeOffset("UTC-11", "UTC-11", TimeSpan.Parse("-11:00:00"));
			AddUtcTimeOffset("Aleutian Standard Time", "알류샨 표준시", TimeSpan.Parse("-10:00:00"));
			AddUtcTimeOffset("Hawaiian Standard Time", "하와이 표준시", TimeSpan.Parse("-10:00:00"));
			AddUtcTimeOffset("Marquesas Standard Time", "마키저스 표준시", TimeSpan.Parse("-09:30:00"));
			AddUtcTimeOffset("Alaskan Standard Time", "알래스카 표준시", TimeSpan.Parse("-09:00:00"));
			AddUtcTimeOffset("UTC-09", "UTC-09", TimeSpan.Parse("-09:00:00"));
			AddUtcTimeOffset("Pacific Standard Time (Mexico)", "태평양 표준시(멕시코)", TimeSpan.Parse("-08:00:00"));
			AddUtcTimeOffset("Pacific Standard Time", "태평양 표준시", TimeSpan.Parse("-08:00:00"));
			AddUtcTimeOffset("UTC-08", "UTC-08", TimeSpan.Parse("-08:00:00"));
			AddUtcTimeOffset("Mountain Standard Time", "산지 표준시", TimeSpan.Parse("-07:00:00"));
			AddUtcTimeOffset("US Mountain Standard Time", "미국 산지 표준시", TimeSpan.Parse("-07:00:00"));
			AddUtcTimeOffset("Mountain Standard Time (Mexico)", "산지 표준시(멕시코)", TimeSpan.Parse("-07:00:00"));
			AddUtcTimeOffset("Central Standard Time (Mexico)", "중부 표준시(멕시코)", TimeSpan.Parse("-06:00:00"));
			AddUtcTimeOffset("Canada Central Standard Time", "캐나다 중부 표준시", TimeSpan.Parse("-06:00:00"));
			AddUtcTimeOffset("Easter Island Standard Time", "이스터 섬 표준시", TimeSpan.Parse("-06:00:00"));
			AddUtcTimeOffset("Central Standard Time", "중부 표준시", TimeSpan.Parse("-06:00:00"));
			AddUtcTimeOffset("Central America Standard Time", "중앙 아메리카 표준시", TimeSpan.Parse("-06:00:00"));
			AddUtcTimeOffset("Eastern Standard Time", "동부 표준시", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("SA Pacific Standard Time", "SA 태평양 표준시", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("Haiti Standard Time", "아이티 표준시", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("US Eastern Standard Time", "미국 동부 표준시", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("Eastern Standard Time (Mexico)", "동부 표준시(멕시코)", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("Cuba Standard Time", "쿠바 표준시", TimeSpan.Parse("-05:00:00"));
			AddUtcTimeOffset("Atlantic Standard Time", "대서양 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Pacific SA Standard Time", "태평??SA 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Paraguay Standard Time", "파라과이 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("SA Western Standard Time", "SA 서부 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Venezuela Standard Time", "베네수엘라 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Central Brazilian Standard Time", "브라질 중부 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Turks And Caicos Standard Time", "터크스 케이커스 표준시", TimeSpan.Parse("-04:00:00"));
			AddUtcTimeOffset("Newfoundland Standard Time", "뉴펀들랜드 표준시", TimeSpan.Parse("-03:30:00"));
			AddUtcTimeOffset("Greenland Standard Time", "그린란드 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Montevideo Standard Time", "몬테비디오 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Argentina Standard Time", "아르헨티나 표준 시간", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("E. South America Standard Time", "동남부 아메리카 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Bahia Standard Time", "바이아 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Saint Pierre Standard Time", "생피에르 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Tocantins Standard Time", "토칸칭스 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("SA Eastern Standard Time", "SA 동부 표준시", TimeSpan.Parse("-03:00:00"));
			AddUtcTimeOffset("Mid-Atlantic Standard Time", "중부-대서양 표준시", TimeSpan.Parse("-02:00:00"));
			AddUtcTimeOffset("UTC-02", "UTC-02", TimeSpan.Parse("-02:00:00"));
			AddUtcTimeOffset("Azores Standard Time", "아조레스 표준시", TimeSpan.Parse("-01:00:00"));
			AddUtcTimeOffset("Cape Verde Standard Time", "카보베르데 표준 시간", TimeSpan.Parse("-01:00:00"));
			AddUtcTimeOffset("UTC", "협정 세계시", TimeSpan.Parse("00:00:00"));
			AddUtcTimeOffset("GMT Standard Time", "GMT 표준시", TimeSpan.Parse("00:00:00"));
			AddUtcTimeOffset("Greenwich Standard Time", "그리니치 표준시", TimeSpan.Parse("00:00:00"));
			AddUtcTimeOffset("Morocco Standard Time", "모로코 표준 시간", TimeSpan.Parse("00:00:00"));
			AddUtcTimeOffset("Central Europe Standard Time", "중앙 유럽 표준시 ", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("Romance Standard Time", "로망스 표준시", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("Namibia Standard Time", "나미비아 표준시", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("Central European Standard Time", "중앙 유럽 표준시", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("W. Central Africa Standard Time", "서중앙 아프리카 표준시", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("W. Europe Standard Time", "서유럽 표준시", TimeSpan.Parse("01:00:00"));
			AddUtcTimeOffset("West Bank Standard Time", "팔레스타인 영토 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Syria Standard Time", "시리아 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Middle East Standard Time", "중동 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("GTB Standard Time", "GTB 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Jordan Standard Time", "요르단 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Israel Standard Time", "예루살렘 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Egypt Standard Time", "이집트 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Kaliningrad Standard Time", "러시아 TZ 1 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("E. Europe Standard Time", "동유럽 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("Libya Standard Time", "리비아 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("South Africa Standard Time", "남아프리카 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("FLE Standard Time", "FLE 표준시", TimeSpan.Parse("02:00:00"));
			AddUtcTimeOffset("E. Africa Standard Time", "동아프리카 표준시", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Russian Standard Time", "러시아 TZ 2 표준시", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Belarus Standard Time", "벨로루시 표준시", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Arabic Standard Time", "아랍 표준시", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Turkey Standard Time", "터키 표준 시간", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Arab Standard Time", "아랍 표준시 ", TimeSpan.Parse("03:00:00"));
			AddUtcTimeOffset("Iran Standard Time", "이란 표준시", TimeSpan.Parse("03:30:00"));
			AddUtcTimeOffset("Azerbaijan Standard Time", "아제르바이잔 표준시", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Arabian Standard Time", "아랍 표준시  ", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Astrakhan Standard Time", "아스트라한 표준시", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Caucasus Standard Time", "코코서스 표준시", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Russia Time Zone 3", "러시아 TZ 3 표준시", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Georgian Standard Time", "그루지야 표준시", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Mauritius Standard Time", "모리셔스 표준 시간", TimeSpan.Parse("04:00:00"));
			AddUtcTimeOffset("Afghanistan Standard Time", "아프가니스탄 표준시", TimeSpan.Parse("04:30:00"));
			AddUtcTimeOffset("West Asia Standard Time", "서아시아 표준시 ", TimeSpan.Parse("05:00:00"));
			AddUtcTimeOffset("Ekaterinburg Standard Time", "러시아 TZ 4 표준시", TimeSpan.Parse("05:00:00"));
			AddUtcTimeOffset("Pakistan Standard Time", "파키스탄 표준 시간", TimeSpan.Parse("05:00:00"));
			AddUtcTimeOffset("Sri Lanka Standard Time", "스리랑카 표준시", TimeSpan.Parse("05:30:00"));
			AddUtcTimeOffset("India Standard Time", "인도 표준시", TimeSpan.Parse("05:30:00"));
			AddUtcTimeOffset("Nepal Standard Time", "네팔 표준시", TimeSpan.Parse("05:45:00"));
			AddUtcTimeOffset("Bangladesh Standard Time", "방글라데시 표준시", TimeSpan.Parse("06:00:00"));
			AddUtcTimeOffset("Central Asia Standard Time", "중앙 아시아 표준시", TimeSpan.Parse("06:00:00"));
			AddUtcTimeOffset("Omsk Standard Time", "옴스크 표준시", TimeSpan.Parse("06:00:00"));
			AddUtcTimeOffset("Myanmar Standard Time", "미얀마 표준시", TimeSpan.Parse("06:30:00"));
			AddUtcTimeOffset("N. Central Asia Standard Time", "노보시비르스크 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("Altai Standard Time", "알타이 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("SE Asia Standard Time", "동남 아시아 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("North Asia Standard Time", "러시아 TZ 6 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("Tomsk Standard Time", "톰스크 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("W. Mongolia Standard Time", "서몽골 표준시", TimeSpan.Parse("07:00:00"));
			AddUtcTimeOffset("China Standard Time", "중국 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("Ulaanbaatar Standard Time", "울란바토르 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("North Asia East Standard Time", "러시아 TZ 7 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("Singapore Standard Time", "말레이 반도 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("Taipei Standard Time", "타이베이 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("W. Australia Standard Time", "서부 오스트레일리아 표준시", TimeSpan.Parse("08:00:00"));
			AddUtcTimeOffset("North Korea Standard Time", "북한 표준시", TimeSpan.Parse("08:30:00"));
			AddUtcTimeOffset("Aus Central W. Standard Time", "오스트레일리아 중부 표준시", TimeSpan.Parse("08:45:00"));
			AddUtcTimeOffset("Korea Standard Time", "대한민국 표준시", TimeSpan.Parse("09:00:00"));
			AddUtcTimeOffset("Yakutsk Standard Time", "러시아 TZ 8 표준시", TimeSpan.Parse("09:00:00"));
			AddUtcTimeOffset("Tokyo Standard Time", "도쿄 표준시", TimeSpan.Parse("09:00:00"));
			AddUtcTimeOffset("Transbaikal Standard Time", "트란스바이칼 표준시", TimeSpan.Parse("09:00:00"));
			AddUtcTimeOffset("AUS Central Standard Time", "오스트레일리아 중부 표준시", TimeSpan.Parse("09:30:00"));
			AddUtcTimeOffset("Cen. Australia Standard Time", "중부 오스트레일리아 표준시", TimeSpan.Parse("09:30:00"));
			AddUtcTimeOffset("West Pacific Standard Time", "서아시아 표준시", TimeSpan.Parse("10:00:00"));
			AddUtcTimeOffset("E. Australia Standard Time", "동부 오스트레일리아 표준시", TimeSpan.Parse("10:00:00"));
			AddUtcTimeOffset("Vladivostok Standard Time", "러시아 TZ 9 표준시", TimeSpan.Parse("10:00:00"));
			AddUtcTimeOffset("AUS Eastern Standard Time", "오스트레일리아 동부 표준시", TimeSpan.Parse("10:00:00"));
			AddUtcTimeOffset("Tasmania Standard Time", "태즈메이니아 표준시", TimeSpan.Parse("10:00:00"));
			AddUtcTimeOffset("Lord Howe Standard Time", "로드하우 표준시", TimeSpan.Parse("10:30:00"));
			AddUtcTimeOffset("Norfolk Standard Time", "노퍽 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Magadan Standard Time", "마가단 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Bougainville Standard Time", "부건빌 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Sakhalin Standard Time", "사할린 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Central Pacific Standard Time", "중앙 태평양 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Russia Time Zone 10", "러시아 TZ 10 표준시", TimeSpan.Parse("11:00:00"));
			AddUtcTimeOffset("Russia Time Zone 11", "러시아 TZ 11 표준시", TimeSpan.Parse("12:00:00"));
			AddUtcTimeOffset("New Zealand Standard Time", "뉴질랜드 표준시", TimeSpan.Parse("12:00:00"));
			AddUtcTimeOffset("Kamchatka Standard Time", "캄차카 반도 표준시", TimeSpan.Parse("12:00:00"));
			AddUtcTimeOffset("Fiji Standard Time", "피지 표준시", TimeSpan.Parse("12:00:00"));
			AddUtcTimeOffset("UTC+12", "UTC+12", TimeSpan.Parse("12:00:00"));
			AddUtcTimeOffset("Chatham Islands Standard Time", "채텀 섬 표준시", TimeSpan.Parse("12:45:00"));
			AddUtcTimeOffset("Tonga Standard Time", "통가 표준시", TimeSpan.Parse("13:00:00"));
			AddUtcTimeOffset("Samoa Standard Time", "사모아 표준시", TimeSpan.Parse("13:00:00"));
			AddUtcTimeOffset("Line Islands Standard Time", "라인 제도 표준시", TimeSpan.Parse("14:00:00"));
		}


		public static bool TryGetUtcTimeOffset(string id, out TimeSpan outTime)
		{
			InitUtcTimeOffset();
			return _utcTimeOffsets.TryGetValue(id, out outTime);
		}

		public static TimeSpan GetTimeZoneOffset(string sourceTimeZoneId, string destinationTimeZoneId)
		{
			TimeSpan srcTimeOffset, dstTimeOffset;
			TryGetUtcTimeOffset(sourceTimeZoneId, out srcTimeOffset);
			TryGetUtcTimeOffset(destinationTimeZoneId, out dstTimeOffset);
			return dstTimeOffset - srcTimeOffset;
		}

		public static DateTime ConvertTimeZone(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			TimeSpan srcTimeOffset, dstTimeOffset;
			TryGetUtcTimeOffset(sourceTimeZoneId, out srcTimeOffset);
			TryGetUtcTimeOffset(destinationTimeZoneId, out dstTimeOffset);
			
			DateTime dt = new DateTime(dateTime.Ticks + (dstTimeOffset - srcTimeOffset).Ticks, DateTimeKind.Unspecified);
			return dt;
		}

		#endregion // UTC Time Offset

		public static float FpsToMS(int fps)
		{
			if (fps == 0.0f)
				return 0.0f;

			return 1.0f / fps;
		}

		public static double GetDeltaTimeMS(DateTime sTime, DateTime eTime)
		{
			return eTime.Subtract(sTime).TotalMilliseconds;
		}

		public static double MSToSec(double ms)
		{
			return (ms * 0.001);
		}

		public static double GetDeltaTimeSec(DateTime sTime, DateTime eTime)
		{
			return eTime.Subtract(sTime).TotalMilliseconds * 0.001;
		}

		public static bool IsDeltaTimeOver(DateTime sTime, DateTime eTime, float time)
		{
			return GetDeltaTimeSec(sTime, eTime) >= time;
		}

		public static bool IsInRange(this DateTime target, DateTime from, DateTime to)
		{
			return target >= from && target <= to;
		}

		// 시간 정보를 추가하거나, 이미 지났으면 현재시간 기준으로 추가 시간을 지급
		public static DateTime IncreaseOrRefreshTime(DateTime time, DateTime curTime, int increaseSeconds)
		{
			DateTime newTime = time;

			if (newTime < curTime)
			{
				newTime = curTime;
			}
			newTime = newTime.Add(TimeSpan.FromSeconds(increaseSeconds));
			return newTime;
		}

		public static DateTime UtcTimeToKorTime(this DateTime utcTime)
		{
			DateTime dt = utcTime.AddHours(9);
			return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, DateTimeKind.Unspecified);
		}

		public static DateTime KorTimeToUtcTime(this DateTime korTime)
		{
			DateTime dt = korTime.Subtract(TimeSpan.FromHours(9));
			return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond, DateTimeKind.Utc);
		}

		public static TimeSpan KorTimeToUtcTime(this TimeSpan korTime)
		{
			return DateTime.UtcNow
							.Date
							.AddMilliseconds(korTime.TotalMilliseconds)
							.Subtract(TimeSpan.FromHours(9))
							.TimeOfDay;
		}

		public static bool IsTimeOver(DateTime checkTime, DateTime curTime)
		{
			return checkTime < curTime;
		}
        
		public static string ToStringDate(this DateTime target)
		{
			return target.ToString("yyyy-MM-dd");
		}
		public static string ToStringDT(this DateTime target)
        {
            return target.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string ToStringHtml5(this DateTime target)
        {
            return target.ToString("yyyy-MM-ddTHH:mm:ss");
        }
        public static string ToStringXRPDateFormat(this DateTime target)
        {
            return target.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public static TimeSpan GetTimeZoneOffset()
        {
            var utcNow = DateTime.UtcNow;
            var localTime = utcNow.ToLocalTime();
            return localTime - utcNow;
        }

        public static void Wait(float time)
        {
            DateTime sTime = DateTime.UtcNow;

            while( true )
            {
                double deltaTimeSec = GetDeltaTimeSec(sTime, DateTime.UtcNow);

                if (deltaTimeSec >= time)
                    return;
            }
        }

        public static void WaitForThreadStop(Thread thread, double timeoutSec)
        {
            if( Thread.CurrentThread == thread )
            {
                return;
            }

            DateTime sTime = DateTime.UtcNow;
            while (thread.ThreadState != ThreadState.Stopped)
            {
                double deltaTimeSec = GetDeltaTimeSec(sTime, DateTime.UtcNow);

                if (timeoutSec >= 0 && deltaTimeSec >= timeoutSec)
                {
                    return;
                }

                Thread.Sleep(1);
            }
        }


		#region Extension
		public static DateTime UnixTimeStampToDateTimeSeconds(this double unixTimeStampSeconds)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStampSeconds);
			return dtDateTime;
		}

		public static DateTime UnixTimeStampToDateTimeMilliseconds(this double unixTimeStampMilliseconds)
		{
			// Unix timestamp is seconds past epoch
			System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddMilliseconds(unixTimeStampMilliseconds);
			return dtDateTime;
		}

		public static double UnixTimestampFromDateTimeSeconds(this DateTime dt)
		{
			return (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		}

		public static double UnixTimestampFromDateTimeMilliseconds(this DateTime dt)
		{
			return (dt - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
		}
		#endregion	// Extension
	}
}
