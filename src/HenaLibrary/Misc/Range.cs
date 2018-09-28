using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Hena
{
	public struct Range<T> where T : struct, IComparable
	{
		public T min;
		public T max;

		public Range(T min_, T max_)
		{
			this.min = min_;
			this.max = max_;
		}
	}

	public static class TRangeEx
	{
		public static int RandomRange(this Range<int> target)
		{
			return RandomEx.Range(target.min, target.max);
		}
		public static float RandomRange(this Range<float> target)
		{
			return RandomEx.Range(target.min, target.max);
		}
		public static double RandomRange(this Range<double> target)
		{
			return RandomEx.Range(target.min, target.max);
		}

		public static bool IsInRange(this Range<int> target, int value)
		{
			return value >= target.min && value <= target.max;
		}

		public static bool IsInRange(this Range<float> target, float value)
		{
			return value >= target.min && value <= target.max;
		}

		public static bool IsInRange(this Range<double> target, double value)
		{
			return value >= target.min && value <= target.max;
		}

		public static bool IsInRange(this Range<decimal> target, decimal value)
		{
			return value >= target.min && value <= target.max;
		}

		public static Range<int> Add(this Range<int> target, Range<int> value)
		{
			return new Range<int>(target.min + value.min, target.max + value.max);
		}

	}

	public struct RangeFloat
	{
		public float min;
		public float max;

		public float Size
		{
			get { return max - min; }
		}

		public RangeFloat(float min_, float max_)
		{
			this.min = min_;
			this.max = max_;
		}

		public float RandomRange()
		{
			return RandomEx.Range(min, max);
		}
	}

	public struct RangeValueFloat
	{
		public float min;
		public float max;
		public float value;

		public float Size
		{
			get { return max - min; }
		}

		public RangeValueFloat(float min_, float max_, float value_)
		{
			this.min = min_;
			this.max = max_;
			this.value = System.Math.Min(System.Math.Max(min_, value_), max_);
		}

		public float SetupRandomValue()
		{
			return value = MakeRandomValue();
		}

		public float MakeRandomValue()
		{
			return RandomEx.Range(min, max);
		}

		public float IncreaseValue(float increaseValue)
		{
			return this.value = System.Math.Min(System.Math.Max(min, value + increaseValue), max);
		}
	}
	

	public struct EaseRange<T>
	{
		public T min;
		public T max;
		public string Ease;
	}
}