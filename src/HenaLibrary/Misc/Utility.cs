using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Hena
{
	public static class Utility
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public static void CreateDirectory(string filename)
		{
			Directory.CreateDirectory((new FileInfo(filename)).Directory.FullName);
		}

		public static void DeleteFile(string path)
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
		public static void DeleteDirectory(string path, bool recursive)
		{
			path = (new FileInfo(path)).Directory.FullName;
			if (Directory.Exists(path))
			{
				Directory.Delete(path, recursive);
			}
		}

		public static void Shuffle<T>(T[] array)
		{
			int n = array.Length;
			for (int i = 0; i < n; i++)
			{
				int idx = RandomEx.Range(i, n - 1);
				Swap(ref array[i], ref array[idx]);
			}
		}

		public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
		{
			var type = enumVal.GetType();
			var memInfo = type.GetMember(enumVal.ToString());
			var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
			return (attributes.Length > 0) ? (T)attributes[0] : null;
		}

		public static string GetDescription(this Enum enumVal)
		{
			DescriptionAttribute descriptionAttribute = Utility.GetAttributeOfType<DescriptionAttribute>(enumVal);
			if (descriptionAttribute != null)
			{
				return descriptionAttribute.Description;
			}
			return string.Empty;
		}

		#region Event
		public static bool InvokeSafe(Action callback)
		{
			try
			{
				if (callback != null)
				{
					(callback.Clone() as Action).Invoke();
				}
			}
			catch (Exception ex)
			{
				if (logger != null) { logger.Error(ex); }
				return false;
			}

			return true;
		}
		public static bool InvokeSafe<T>(Action<T> callback, T p1)
		{
			try
			{
				if (callback != null)
				{
					(callback.Clone() as Action<T>).Invoke(p1);
				}
			}
			catch (Exception ex)
			{
				if (logger != null) { logger.Error(ex); }
				return false;
			}

			return true;
		}
		public static bool InvokeSafe<T1, T2>(Action<T1, T2> callback, T1 p1, T2 p2)
		{
			try
			{
				if (callback != null)
				{
					(callback.Clone() as Action<T1, T2>).Invoke(p1, p2);
				}
			}
			catch (Exception ex)
			{
				if (logger != null) { logger.Error(ex); }
				return false;
			}
			return true;
		}
		public static bool InvokeSafe<T1, T2, T3>(Action<T1, T2, T3> callback, T1 p1, T2 p2, T3 p3)
		{
			try
			{
				if (callback != null)
				{
					(callback.Clone() as Action<T1, T2, T3>).Invoke(p1, p2, p3);
				}
			}
			catch (Exception ex)
			{
				if (logger != null) { logger.Error(ex); }
				return false;
			}
			return true;

		}
		public static bool InvokeSafe<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			try
			{
				if (callback != null)
				{
					(callback.Clone() as Action<T1, T2, T3, T4>).Invoke(p1, p2, p3, p4);
				}
			}
			catch (Exception ex)
			{
				if (logger != null) { logger.Error(ex); }
				return false;
			}
			return true;

		}
		#endregion // Event

		#region Array Math
		public static int Sum<T>(ICollection<T> collections, Func<T, int> match)
		{
			int v = 0;
			foreach (var it in collections) { v += match.Invoke(it); }
			return v;
		}
		public static long Sum<T>(ICollection<T> collections, Func<T, long> match)
		{
			long v = 0;
			foreach (var it in collections) { v += match.Invoke(it); }
			return v;
		}
		public static decimal Sum<T>(ICollection<T> collections, Func<T, decimal> match)
		{
			decimal v = 0m;
			foreach (var it in collections) { v += match.Invoke(it); }
			return v;
		}
		public static float Sum<T>(ICollection<T> collections, Func<T, float> match)
		{
			float v = 0f;
			foreach (var it in collections) { v += match.Invoke(it); }
			return v;
		}
		public static double Sum<T>(ICollection<T> collections, Func<T, double> match)
		{
			double v = 0f;
			foreach (var it in collections) { v += match.Invoke(it); }
			return v;
		}
		// Sum
		public static int Sum(params byte[] items)
		{
			int t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static int Sum(params short[] items)
		{
			int t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static int Sum(params int[] items)
		{
			int t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static float Sum(params float[] items)
		{
			float t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static double Sum(params double[] items)
		{
			double t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static long Sum(params long[] items)
		{
			long t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static decimal Sum(params decimal[] items)
		{
			decimal t = 0;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}

		// Mul
		public static float Mul(params byte[] items)
		{
			float t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t *= items[i]; }
			return t;
		}
		public static float Mul(params short[] items)
		{
			float t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t *= items[i]; }
			return t;
		}
		public static float Mul(params int[] items)
		{
			float t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t *= items[i]; }
			return t;
		}
		public static float Mul(params float[] items)
		{
			float t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t *= items[i]; }
			return t;
		}
		public static double Mul(params double[] items)
		{
			double t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}
		public static double Mul(params long[] items)
		{
			double t = 1;
			for (int i = items.Length - 1; i >= 0; --i) { t += items[i]; }
			return t;
		}

		// Average
		public static float Average(params byte[] items)
		{
			return items.Length == 0 ? 0f : (float)Sum(items) / items.Length;
		}
		public static float Average(params short[] items)
		{
			return items.Length == 0 ? 0f : (float)Sum(items) / items.Length;
		}
		public static float Average(params int[] items)
		{
			return items.Length == 0 ? 0f : (float)Sum(items) / items.Length;
		}
		public static float Average(params float[] items)
		{
			return items.Length == 0 ? 0f : (float)Sum(items) / items.Length;
		}
		public static double Average(params double[] items)
		{
			return items.Length == 0 ? 0f : Sum(items) / items.Length;
		}
		public static double Average(params long[] items)
		{
			return items.Length == 0 ? 0f : (double)Sum(items) / items.Length;
		}
		public static decimal Average(params decimal[] items)
		{
			return items.Length == 0 ? 0m : Sum(items) / items.Length;
		}


		#endregion

		#region Array / Container
		public static T[] NewArray<T>(int count, bool createInstance) where T : class, new()
		{
			T[] instances = new T[count];
			if (createInstance)
			{
				for (int i = 0; i < count; ++i)
				{
					instances[i] = new T();
				}
			}
			return instances;
		}
		public static void MakeUnique<T>(ref List<T> target)
		{
			HashSet<T> hs = new HashSet<T>();
			for (int i = target.Count - 1; i >= 0; --i)
			{
				if (hs.Contains(target[i]) == false)
				{
					hs.Add(target[i]);
					continue;
				}
				target.RemoveAt(i);
			}
		}

		public static bool IsNullOrEmpty<T>(T[] array)
		{
			return array == null || array.Length == 0;
		}

		public static bool IsNullOrEmpty<T>(ICollection<T> target)
		{
			return target == null || target.Count == 0;
		}

		public static bool IsNullOrEmpty<TKey, TValue>(IDictionary<TKey, TValue> target)
		{
			return target == null || target.Count == 0;
		}

		public static bool IsNullOrEmptyAnyItem<T>(T[] target)
		{
			if (target == null || target.Length == 0)
				return true;

			foreach (var it in target)
			{
				if (it == null)
					return true;
			}
				
			return false;
		}

		public static bool IsNullOrEmptyAnyItem<T>(ICollection<T> target)
		{
			if (target == null || target.Count == 0)
				return true;

			foreach (var it in target)
			{
				if (it == null)
					return true;
			}

			return false;
		}

		public static bool IsNullOrEmptyAnyItem<TKey, TValue>(IDictionary<TKey, TValue> target)
		{
			if (target == null || target.Count == 0)
				return true;

			foreach (var it in target)
			{
				if (it.Value == null)
					return true;
			}

			return false;
		}

		public static T[] Combine<T>(T[] array, T[] target, bool newArray)
		{
			int srcLength = array.Length;
			int targetLength = target.Length;
			if (newArray)
			{
				T[] outArray = new T[srcLength + targetLength];
				Buffer.BlockCopy(array, 0, outArray, 0, srcLength);
				Buffer.BlockCopy(target, 0, outArray, srcLength, targetLength);
				return outArray;
			}
			else
			{
				Array.Resize(ref array, srcLength + targetLength);
				Buffer.BlockCopy(target, 0, array, srcLength, targetLength);
				return array;
			}
		}

		public static T[] Split<T>(T[] array, int offset, int count)
		{
			T[] outArray = new T[count];
			Buffer.BlockCopy(array, offset, outArray, 0, count);
			return outArray;
		}

		public static T[] Shift<T>(ref T[] array, int shiftOffset, T defaultValue = default(T))
		{
			int shiftCount = System.Math.Abs(shiftOffset);
			if (array.Length < shiftCount)
				return null;

			T[] outItems = new T[shiftCount];
			if (shiftOffset > 0)
			{
				Array.Copy(array, array.Length - shiftCount, outItems, 0, shiftCount);
				Array.Copy(array, 0, array, shiftCount, array.Length - shiftCount);
				SetValue(ref array, defaultValue, 0, shiftCount);
			}
			else
			{
				Array.Copy(array, 0, outItems, 0, shiftCount);
				Array.Copy(array, shiftCount, array, 0, array.Length - shiftCount);
				SetValue(ref array, defaultValue, array.Length - shiftCount, shiftCount);
			}
			return outItems;
		}

		public static bool Contains<T>(T[] array, T value)
		{
			if (array == null)
				return false;

			int l = array.Length;
			for (int i = 0; i < l; ++i)
			{
				if (array[i].Equals(value))
					return true;
			}
			return false;
		}
		public static T[] CountingSortAsc<T>(T[] array)
		{
			Dictionary<T, int> counter;
			return CountingSort(array, out counter, true);
		}
		public static T[] CountingSortAsc<T>(T[] array, out Dictionary<T, int> counter)
		{
			return CountingSort(array, out counter, true);
		}

		public static T[] CountingSortDesc<T>(T[] array)
		{
			Dictionary<T, int> counter;
			return CountingSort(array, out counter, false);
		}
		public static T[] CountingSortDesc<T>(T[] array, out Dictionary<T, int> counter)
		{
			return CountingSort(array, out counter, false);
		}

		public static T[] CountingSort<T>(T[] array, out Dictionary<T, int> outCounter, bool isAsc)
		{
			T[] newArray = new T[array.Length];
			array.CopyTo(newArray, 0);

			Dictionary<T, int> counter = outCounter = new Dictionary<T, int>();
			for (int i = 0; i < array.Length; ++i)
			{
				if (counter.ContainsKey(array[i]))
				{
					counter[array[i]] += 1;
				}
				else
				{
					counter.Add(array[i], 1);
				}
			}

			if (isAsc)
			{
				Array.Sort(newArray, (T left, T right) =>
				{
					int lCount = counter[left];
					int rCount = counter[right];
					if (lCount > rCount)
						return -1;
					if (lCount < rCount)
						return 1;
					return 0;
				});
			}
			else
			{
				Array.Sort(newArray, (T left, T right) =>
				{
					int lCount = counter[left];
					int rCount = counter[right];
					if (lCount > rCount)
						return 1;
					if (lCount < rCount)
						return -1;
					return 0;
				});
			}

			return newArray;
		}

		public static Dictionary<T, int> Counting<T>(T[] array)
		{
			Dictionary<T, int> counter = new Dictionary<T, int>();
			for (int i = 0; i < array.Length; ++i)
			{
				if (array[i] == null)
					continue;
				if (counter.ContainsKey(array[i]))
				{
					counter[array[i]] += 1;
				}
				else
				{
					counter.Add(array[i], 1);
				}
			}
			return counter;
		}

		public static T[] ToBestCountObjects<T>(T[] array) where T : class
		{
			Dictionary<T, int> counter = Counting(array);

			int bestCount = 0;
			var it = counter.GetEnumerator();
			while (it.MoveNext())
			{
				if (it.Current.Key == null)
					continue;

				bestCount = System.Math.Max(it.Current.Value, bestCount);
			}

			List<T> result = new List<T>();
			it = counter.GetEnumerator();
			while (it.MoveNext())
			{
				if (it.Current.Value == bestCount)
				{
					if (it.Current.Key == null)
						continue;
					result.Add(it.Current.Key);
				}
			}
			return result.ToArray();
		}

		public static void AddRange<T>(this ICollection<T> target, T[] values)
		{
			for (int i = 0; i < values.Length; ++i)
				target.Add(values[i]);
		}

		public static void AddRange<T>(this ICollection<T> target, ICollection<T> values)
		{
			foreach (var it in values)
				target.Add(it);
		}

		public static void AddRangeSafe<T>(this ICollection<T> target, T[] values)
		{
            if (values == null)
                return;

            foreach ( var it in values)
			{
				if( target.Contains(it) == false )
				{
					target.Add(it);
				}
			}
		}

		public static void AddRangeSafe<T>(this ICollection<T> target, ICollection<T> values)
		{
            if (values == null)
                return;
			foreach (var it in values)
			{
				if (target.Contains(it) == false)
				{
					target.Add(it);
				}
			}
		}

		public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> target, IDictionary<TKey, TValue> values)
		{
			foreach (var it in values)
			{
				target.Add(it.Key, it.Value);
			}
		}

		public static void AddRangeSafe<TKey, TValue>(this Dictionary<TKey, TValue> target, IDictionary<TKey, TValue> values)
		{
			if (values == null)
				return;

			foreach(var it in values)
			{
				target.Add(it.Key, it.Value);
			}
		}

		public static void AddRangeClone<TKey, TValue>(this Dictionary<TKey, TValue> target, IDictionary<TKey, TValue> values)
			where TValue : ICloneable<TValue>
		{
			var it = values.GetEnumerator();
			while (it.MoveNext())
			{
				target.Add(it.Current.Key, it.Current.Value.Clone());
			}
		}

		public static void RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey[] list)
		{
			for (int i = 0; i < list.Length; ++i)
			{
				target.Remove(list[i]);
			}
		}

		public static void RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> target, List<TKey> list, bool bClearKey)
		{
			for (int i = 0; i < list.Count; ++i)
			{
				target.Remove(list[i]);
			}

			if (bClearKey)
				list.Clear();
		}

		public static T[] ToArray<T>(this ICollection<T> collection)
		{
			T[] values = new T[collection.Count];
			collection.CopyTo(values, 0);
			return values;
		}
		

		#endregion

		#region MinMax
		public static bool IsInRange(byte min, byte max, byte value)
		{
			return value >= min && max >= value;
		}
		public static bool IsInRange(short min, short max, short value)
		{
			return value >= min && max >= value;
		}
		public static bool IsInRange(int min, int max, int value)
		{
			return value >= min && max >= value;
		}
		public static bool IsInRange(long min, long max, long value)
		{
			return value >= min && max >= value;
		}
		public static bool IsInRange(float min, float max, float value)
		{
			return value >= min && max >= value;
		}
		public static bool IsInRange(double min, double max, double value)
		{
			return value >= min && max >= value;
		}
		#endregion

		#region SetValue
		public static void SetValue<T>(ref T[] t, T value)
		{
			for (int i = 0; i < t.Length; ++i)
			{
				t[i] = value;
			}
		}

		public static void SetValue<T>(ref T[] t, T value, int position, int count)
		{
			int end = Math.Min(position + count, t.Length);
			for (int i = position; i < end; ++i)
			{
				t[i] = value;
			}
		}
		#endregion

		#region Equals
		public static bool Equals<T>(ref T[] t1, ref T[] t2) where T : IComparable<T>
		{
			if (t1.Length != t2.Length)
				return false;

			for (int i = 0; i < t1.Length; ++i)
			{
				if (t1[i].Equals(t2[i]) == false)
					return false;
			}
			return true;
		}
		#endregion

		#region Lerp
		public static bool Lerp(ref float[] target, ref float[] destination, float t)
		{
			if (target.Length != destination.Length)
				return false;

			bool result = false;
			for (int i = 0; i < target.Length; ++i)
			{
				if (target[i] == destination[i])
					continue;

				target[i] = Mathf.Lerp(target[i], destination[i], t);


				if (Mathf.Abs(target[i] - destination[i]) <= 0.0002f)
				{
					target[i] = destination[i];
				}
				result = true;
			}
			return result;
		}
		#endregion

		#region Copy
		public static void Swap(IList list, int idxA, int idxB)
		{
			object temp = list[idxA];
			list[idxA] = list[idxB];
			list[idxB] = temp;
		}
		public static void Swap<T>(IList<T> list, int idxA, int idxB)
		{
			T temp = list[idxA];
			list[idxA] = list[idxB];
			list[idxB] = temp;
		}

		public static void Swap<T>(ref T v1, ref T v2)
		{
			T temp = v1;
			v1 = v2;
			v2 = temp;
		}

		public static void CopyToClone<T>(List<T> src, List<T> target) where T : ICloneable<T>
		{
			target.Clear();
			for (int i = 0; i < src.Count; ++i)
			{
				target.Add(src[i].Clone());
			}
		}
		public static void CopyToClone<T>(T[] src, List<T> target) where T : ICloneable<T>
		{
			target.Clear();
			for (int i = 0; i < src.Length; ++i)
			{
				target.Add(src[i].Clone());
			}
		}

		public static void CopyToClone<TKey, TValue>( Dictionary<TKey, TValue> src,  Dictionary<TKey, TValue> target) where TValue : ICloneable<TValue>
		{
			target.Clear();
			var it = src.GetEnumerator();
			while (it.MoveNext())
			{
				target.Add(it.Current.Key, it.Current.Value.Clone());
			}
		}
		#endregion

		#region Alphabet
		private static char[] _alphabets = null;
		public static string GetColumnName(int index)
		{
			if (_alphabets == null)
			{
				_alphabets = MakeAlphabet();
			}

			int alphabetsCount = _alphabets.Length;
			string result = string.Empty;
			while (true)
			{
				result = result.Insert(0, _alphabets[index % (alphabetsCount)].ToString());
				index = (index) / alphabetsCount;
				if (index <= 0)
					break;
				--index;
			}

			return result;
		}

		public static char[] MakeAlphabet()
		{
			const int alphabetsCount = 26;
			char[] alphabets = new char[alphabetsCount];
			for (int i = 0; i < alphabetsCount; ++i)
			{
				alphabets[i] = Convert.ToChar(65 + i);
			}
			return alphabets;
		}
		#endregion

		#region Security
		public static string GetEncryptedString(string value, string password)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;

			try
			{
				return AESUtility.Encrypt(value, password);
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}

		public static string GetDecryptedString(string value, string password)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			try
			{
				return AESUtility.Decrypt(value, password);
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}
		#endregion // Security

		#region Enum Extension
		public static string ToUpperString(this Enum en)
		{
			return en.ToString().ToUpper();
		}
		#endregion // Enum Extension
		
		#region Timer Extension
		public static void TryDispose(this System.Threading.Timer timer)
		{
			try { timer.Dispose(); }
			catch (Exception) { }
		}

		public static bool TryDispose(this System.Threading.Timer timer, System.Threading.WaitHandle waitHandle)
		{
			try { return timer.Dispose(waitHandle); }
			catch (Exception) { return false; }
		}
		#endregion // Timer Extension

		#region String Extension
		public static string ToSizeString(this int value) { return ((double)value).ToSizeString(); }
		public static string ToSizeString(this uint value) { return ((double)value).ToSizeString(); }
		public static string ToSizeString(this long value) { return ((double)value).ToSizeString(); }
		public static string ToSizeString(this ulong value) { return ((double)value).ToSizeString(); }
		public static string ToSizeString(this decimal value) { return ((double)value).ToSizeString(); }
		public static string ToSizeString(this double value)
		{
			if (value < 1024)
				return $"{value.ToString("N8")}";

			string textFormat = "N2";
			value /= 1024;
			if (value < 1024)
				return $"{value.ToString(textFormat)}K";

			value /= 1024;
			if (value < 1024)
				return $"{value.ToString(textFormat)}M";

			value /= 1024;
			if (value < 1024)
				return $"{value.ToString(textFormat)}G";

			value /= 1024;
			return $"{value.ToString(textFormat)}T";
		}

        public static string ToCurrencyString(this decimal value, string currencyName, byte decimalLength=8)
        {
            if (currencyName.Equals("KRW"))
            {
                return value.ToString("N0");
            }

            if (currencyName.Equals("USD")
                || currencyName.Equals("USDT")
                || currencyName.Equals("JPY"))
            {
                return value.ToString("N2");
            }

            return value.ToString("N" + decimalLength);
        }

        // 이메일 포멧인지 체크
        public static bool IsEmailFormat(this string value)
        {
			if (value.Length > 256)
				return false;

			if (value.IndexOf("\r\n") >= 0)
				return false;

			if (value.Trim().Contains(" "))
				return false;

			bool valid = Regex.IsMatch(value, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
            return valid;
        }

        // Url에 안전한 Base64 문자열로 변환
        public static string EncodeBase64ToUrlSafeBase64(this string value)
        {
            return value.TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        // Url에 안전한 Base64에서 일반 Base64로 변환
        public static string DecodeUrlSafeBase64ToBase64(this string value)
        {
            int paddingNum = value.Length % 4;
            if (paddingNum != 0)
            {
                paddingNum = 4 - paddingNum;
            }
            return (value + new string('=', paddingNum)).Replace('-', '+').Replace('_', '/');
        }
		#endregion // String Extension
	}
}
