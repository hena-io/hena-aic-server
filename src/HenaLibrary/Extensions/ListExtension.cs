using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class ListExtension
	{
        #region Collection Extensions
        public static string Join_LockThis<T>(this ICollection<T> target, string separator)
        {
            lock (target) { return target.Join(separator); }
        }
        public static string Join<T>(this ICollection<T> target, string separator)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach( var it in target )
            {
                stringBuilder.Append($"{it.ToString()}{separator}");
            }
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - separator.Length, separator.Length);
            }
            return stringBuilder.ToString();
        }
        #endregion  // Collection Extensions

        #region LockThis Extension
        public static void Add_LockThis<TValue>(this ICollection<TValue> target, TValue value)
		{
			lock (target) { target.Add(value); }
		}

		public static void Remove_LockThis<TValue>(this ICollection<TValue> target, TValue value)
		{
			lock (target) { target.Remove(value); }
		}

		public static void Clear_LockThis<TValue>(this ICollection<TValue> target)
		{
			lock (target) { target.Clear(); }
		}

		public static bool Contains_LockThis<TValue>(this ICollection<TValue> target, TValue value)
		{
			lock (target) { return target.Contains(value); }
		}

		public static TValue[] ToArray_LockThis<TValue>(this ICollection<TValue> target)
		{
			lock (target) { return target.ToArray(); }
		}

        public static TEnum[] ToEnumArray<TEnum>(this ICollection<string> target)
            where TEnum : struct
        {
            List<TEnum> items = new List<TEnum>();
            lock (target)
            {
                foreach (var it in target)
                {
                    TEnum en;
                    if (Enum.TryParse(it.Trim(), true, out en))
                    {
                        items.Add(en);
                    }
                }
            }
            return items.ToArray();
        }
        #endregion // LockThis Extension

        public static List<T> SubItems_LockThis<T>(this List<T> container, int offset, int limit)
        {
            lock(container)
            {
                return SubItems<T>(container, offset, limit);
            }
        }

        public static List<T> SubItems<T>(this List<T> container, int offset, int limit)
        {
            List<T> newContainer = new List<T>();
            int count = Math.Min(container.Count, offset + limit);
            for (int i = offset; i < count; ++i)
            {
                newContainer.Add(container[i]);
            }
            return newContainer;
        }
    }
}
