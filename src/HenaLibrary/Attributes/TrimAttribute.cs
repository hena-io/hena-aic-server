using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hena.Library.Attributes
{
	public sealed class TrimAttribute : Attribute
	{ }

	public class TrimConverter<T> : JsonConverter where T : new()
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jObject = JObject.Load(reader);
			var obj = new T();
			serializer.Populate(jObject.CreateReader(), obj);

			var props = objectType.GetFields(BindingFlags.Instance | BindingFlags.Public)
				.Where(p => p.FieldType == typeof(string))
				.Where(p => Attribute.GetCustomAttributes(p).Any(u => (Type)u.TypeId == typeof(TrimAttribute)))
				;

			foreach (var fieldInfo in props)
			{
				var val = (string)fieldInfo.GetValue(obj);
				fieldInfo.SetValue(obj, val.Trim());
			}

			return obj;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.IsAssignableFrom(typeof(T));
		}
	}
}
