using System;
using System.Collections.Generic;

namespace Hena
{
	public class RandomEx
	{
		private static System.Random _random = new System.Random(DateTime.UtcNow.Millisecond);

        public static T Range<T>(List<T> targets)
        {
            return targets[Range(0, targets.Count)];
        }

        public static T Range<T>(T[] targets)
        {
            return targets[Range(0, targets.Length)];
        }
		public static int Range(int min, int max)
		{
			return _random.Next(min, max);
		}

		public static int Range(Range<int> range)
		{
			return Range(range.min, range.max);
		}
		public static long Range(Range<long> range)
		{
			return Range(range.min, range.max);
		}
		public static float Range(Range<float> range)
		{
			return Range(range.min, range.max);
		}
		public static double Range(Range<double> range)
		{
			return Range(range.min, range.max);
		}
		public static long Range(long min, long max)
		{
			return (long)Range((double)min, (double)max);
		}

		public static float Range(float min, float max)
		{
			float r = (float)_random.NextDouble();
			return min + ((max - min) * r);
		}

		public static double Range(double min, double max)
		{
			double r = _random.NextDouble();
			return min + ((max - min) * r);
		}

		public static decimal Range(decimal min, decimal max)
		{
			decimal r = (decimal)_random.NextDouble();
			return min + ((max - min) * r);
		}

		#region Scale Version

		public static int Range(int min, int max, int scale)
		{
			return _random.Next(min * scale, max * scale) / scale;
		}

		public static int Range(Range<int> range, int scale)
		{
			return Range(range.min, range.max, scale);
		}
		public static long Range(Range<long> range, int scale)
		{
			return Range(range.min, range.max, scale);
		}
		public static float Range(Range<float> range, int scale)
		{
			return Range(range.min, range.max, scale);
		}
		public static double Range(Range<double> range, int scale)
		{
			return Range(range.min, range.max, scale);
		}
		public static long Range(long min, long max, int scale)
		{
			return (long)Range((double)min, (double)max, scale);
		}

		public static float Range(float min, float max, int scale)
		{
			min *= scale;
			max *= scale;
			float r = (float)_random.NextDouble();
			float result = min + ((max - min) * r);
			return result / scale;
		}

		public static double Range(double min, double max, int scale)
		{
			min *= scale;
			max *= scale;

			double r = _random.NextDouble();
			double result = min + ((max - min) * r);
			return result / scale;

		}
		#endregion
	}
}
