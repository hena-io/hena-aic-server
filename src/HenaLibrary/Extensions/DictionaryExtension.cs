using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class DictionaryExtension
	{
		#region Extension
		public static TValue[] ToArrayValue<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			TValue[] values = new TValue[target.Count];
			target.Values.CopyTo(values, 0);
			return values;
		}

		public static TKey[] ToArrayKey<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			TKey[] values = new TKey[target.Count];
			target.Keys.CopyTo(values, 0);
			return values;
		}

		public static TValue TryGetValueEx<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey key)
		{
			TValue value;
			target.TryGetValue(key, out value);
			return value;
		}

		public static bool TryGetValueEx<TKey, TValue, TValueAs>(this Dictionary<TKey, TValue> target, TKey key, out TValueAs outValue)
			where TValueAs : class, TValue
		{
			TValue value;
			if (target.TryGetValue(key, out value))
			{
				outValue = value as TValueAs;
				return true;
			}
			else
			{
				outValue = null;
				return false;
			}
		}
		#endregion // Extension

		#region LockThis Extension
		public static int Count_LockThis<TValue>(this ICollection<TValue> target)
		{
			lock (target) { return target.Count; }
		}

		public static int Count_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			lock (target) { return target.Count; }
		}

		public static void Add_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey key, TValue value)
		{
			lock (target) { target.Add(key, value); }
		}

		public static void Remove_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey key)
		{
			lock (target) { target.Remove(key); }
		}

		public static void Clear_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			lock (target) { target.Clear(); }
		}

		public static bool ContainsKey_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey key)
		{
			lock (target) { return target.ContainsKey(key); }
		}

		public static bool ContainsValue_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target, TValue value)
		{
			lock (target) { return target.ContainsValue(value); }
		}

		public static TValue[] ToArrayValue_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			lock (target) { return target.ToArrayValue(); }
		}

		public static TKey[] ToArrayKey_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target)
		{
			lock (target) { return target.ToArrayKey(); }
		}

		public static TValue TryGetValueEx_LockThis<TKey, TValue>(this Dictionary<TKey, TValue> target, TKey key)
		{
			lock (target) { return target.TryGetValueEx(key); }
		}

		public static bool TryGetValueEx_LockThis<TKey, TValue, TValueAs>(this Dictionary<TKey, TValue> target, TKey key, out TValueAs outValue)
			where TValueAs : class, TValue
		{
			lock (target) { return target.TryGetValueEx(key, out outValue); }
		}
		#endregion // LockThis Extension
	}
}
