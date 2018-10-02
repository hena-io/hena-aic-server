using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hena
{
    public static class JSONUtility
    {
        #region GetValue
        public static TEnum GetValueEnum<TEnum>(this JToken token, string propertyName, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            if (token == null)
                return defaultValue;

            JToken value;
            if (((JObject)token).TryGetValue(propertyName, out value))
            {
                TEnum outValue;
                if (Enum.TryParse(value.Value<string>(), true, out outValue))
                {
                    return outValue;
                }
            }
            return defaultValue;
        }

        public static Uri GetValue(this JToken token, string propertyName, Uri defaultValue = default(Uri))
        {
            if (token == null)
                return defaultValue;
            try
            {
                return token[propertyName].AsUri();
            }
            catch (Exception ex)
            {
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
            }
            return defaultValue;

        }

        public static TValue GetValue<TValue>(this JToken token, string propertyName, TValue defaultValue = default(TValue))
        {
            if (token == null)
                return defaultValue;

			try
			{
                JToken value = token[propertyName];
                if (value != null)
                {
					if (typeof(TValue).IsEnum)
					{
						object outValue;
						if (Enum.TryParse(typeof(TValue), value.Value<string>(), true, out outValue))
						{
							return (TValue)outValue;
						}
					}
					else
					{
						if (typeof(TValue) == typeof(DBKey))
							return (TValue)(object)(value.AsDBKey());

						else if (typeof(TValue) == typeof(TimeSpan))
							return (TValue)(object)(value.AsTimeSpan());

						return value.Value<TValue>();
					}
                }
            }
            catch (Exception ex)
            {
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
            }
            return defaultValue;
        }
        #endregion // GetValue

        public static TEnum AsEnum<TEnum>(this JToken token, bool ignoreCase = true) where TEnum : struct
        {
            return token.AsString().ToEnum<TEnum>(ignoreCase);
        }

        public static bool AsEnum<TEnum>(this JToken token, out TEnum outValue, bool ignoreCase = true) where TEnum : struct
        {
            return Enum.TryParse(token.AsString(), ignoreCase, out outValue);
        }

        public static JArray ToJSON<T>(this ICollection<T> collection) where T : IJSONSerializable
		{
			JArray jArray = new JArray();
			foreach (var it in collection)
			{
				jArray.Add(it.ToJSON());
			}
			return jArray;
		}

        public static JArray ToJSON(this ICollection<string> collection)
        {
            JArray jArray = new JArray();
            foreach (var it in collection)
            {
                jArray.Add(it.ToString());
            }
            return jArray;
        }

		public static JObject ToJSON<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> collection)
		{
			JObject jObject = new JObject();
			foreach (var it in collection)
			{
				jObject.Add(it.Key.ToString(), it.Value.ToString());
			}
			return jObject;
		}

		public static JArray ToJSONEnum<TEnum>(this ICollection<TEnum> collection) where TEnum : struct
		{
			JArray jArray = new JArray();
			foreach (var it in collection)
			{
				jArray.Add(it.ToString());
			}
			return jArray;
		}

		public static bool FromJSON<T>(this T[] array, JToken jToken) where T : IJSONSerializable, new()
		{
			JArray jArray = jToken as JArray;
			if (jArray == null)
				return false;

			bool result = true;
			int length = array.Length;
			int idx = 0;
			foreach (var it in jArray)
			{
				if (idx >= length)
					break;

				var item = new T();
				result = item.FromJSON(it) && result;
				array[idx] = item;
				++idx;
			}
			return result;
		}

		public static bool FromJSON<T>(this ICollection<T> collection, JToken jToken) where T : IJSONSerializable, new()
		{
			JArray jArray = jToken as JArray;
			if (jArray == null)
				return false;

			bool result = true;
			foreach (var it in jArray)
			{
				var item = new T();
				result = item.FromJSON(it) && result;
				collection.Add(item);
			}
			return result;
		}

        public static bool FromJSON(this ICollection<string> collection, JToken jToken)
        {
            JArray jArray = jToken as JArray;
            if (jArray == null)
                return false;

            try
            {
                foreach (var it in jArray)
                {
                    collection.Add(it.AsString());
                }
                return true;
            }
            catch (Exception) { return false; }
        }

        public static bool FromJSONEnum<TEnum>(this TEnum[] array, JToken jToken) where TEnum : struct
		{
			JArray jArray = jToken as JArray;
			if (jArray == null)
				return false;

			bool result = true;
			int length = array.Length;
			int idx = 0;
			foreach (var it in jArray)
			{
				if (idx >= length)
					break;

				TEnum item;
				if (it.AsEnum(out item, true) == false)
				{
					result = false;
				}

				array[idx] = item;
				++idx;
			}
			return result;
		}

		public static bool FromJSONEnum<TEnum>(this ICollection<TEnum> collection, JToken jToken) where TEnum : struct
		{
			JArray jArray = jToken as JArray;
			if (jArray == null)
				return false;

			bool result = true;
			foreach (var it in jArray)
			{
				TEnum item;
				if (it.AsEnum(out item, true) == false)
				{
					result = false;
				}
				collection.Add(item);
			}
			return result;
		}

        public static bool FromBinary(this IJSONSerializable target, byte[] bytes)
        {
            if (bytes.Length < sizeof(int))
                return false;

			int checksum = bytes.ToInt32();


			int streamChecksum = bytes.CheckSum(sizeof(int));
			if (checksum != streamChecksum)
                return false;

            Stream stream = new MemoryStream(bytes, sizeof(int), bytes.Length - sizeof(int));
            byte[] decompressed = stream.GZipDecompress();

            string message = Encoding.UTF8.GetString(decompressed);
            JToken jToken = JToken.Parse(message);

            stream.Close();
            return target.FromJSON(jToken);
        }

        public static byte[] ToBinary(this IJSONSerializable target)
        {
            string message = target.ToJSON().ToString();

            byte[] utf8String = Encoding.UTF8.GetBytes(message);
            byte[] compressed = utf8String.GZipCompress();

			int checksum = compressed.CheckSum();
			byte[] result = Utility.Combine(checksum.ToBytes(), compressed, true);
            return result;
        }

        public static bool FromBinaryBase64(this IJSONSerializable target, string base64, bool isUrlSafe = true)
        {
            byte[] bytes = Convert.FromBase64String(isUrlSafe ? base64.DecodeUrlSafeBase64ToBase64() : base64);
            return target.FromBinary(bytes);
        }

        public static string ToBinaryBase64(this IJSONSerializable target, bool isUrlSafe = true)
        {
            byte[] bytes = target.ToBinary();
            return isUrlSafe ? Convert.ToBase64String(bytes).EncodeBase64ToUrlSafeBase64() : Convert.ToBase64String(bytes);
        }
    }
}
