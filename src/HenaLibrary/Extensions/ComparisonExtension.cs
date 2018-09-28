using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class ComparisonExtension
	{
		public static int CompareToAsc(this float left, float right) { return left.CompareTo(right); }
		public static int CompareToAsc(this string left, string right) { return left.CompareTo(right); }
		public static int CompareToAsc(this uint left, uint right) { return left.CompareTo(right); }
		public static int CompareToAsc(this ulong left, ulong right) { return left.CompareTo(right); }
		public static int CompareToAsc(this TimeSpan left, TimeSpan right) { return left.CompareTo(right); }
		public static int CompareToAsc(this bool left, bool right) { return left.CompareTo(right); }
		public static int CompareToAsc(this byte left, byte right) { return left.CompareTo(right); }
		public static int CompareToAsc(this sbyte left, sbyte right) { return left.CompareTo(right); }
		public static int CompareToAsc(this long left, long right) { return left.CompareTo(right); }
		public static int CompareToAsc(this decimal left, decimal right) { return left.CompareTo(right); }
		public static int CompareToAsc(this double left, double right) { return left.CompareTo(right); }
		public static int CompareToAsc(this short left, short right) { return left.CompareTo(right); }
		public static int CompareToAsc(this ushort left, ushort right) { return left.CompareTo(right); }
		public static int CompareToAsc(this int left, int right) { return left.CompareTo(right); }
		public static int CompareToAsc(this DateTime left, DateTime right) { return left.CompareTo(right); }

		public static int CompareToDesc(this float left, float right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this string left, string right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this uint left, uint right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this ulong left, ulong right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this TimeSpan left, TimeSpan right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this bool left, bool right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this byte left, byte right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this sbyte left, sbyte right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this long left, long right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this decimal left, decimal right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this double left, double right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this short left, short right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this ushort left, ushort right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this int left, int right) { return -left.CompareTo(right); }
		public static int CompareToDesc(this DateTime left, DateTime right) { return -left.CompareTo(right); }
	}
}
