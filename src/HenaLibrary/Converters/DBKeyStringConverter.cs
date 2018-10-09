using Newtonsoft.Json;
using System;

namespace Hena
{
	public class DBKeyStringConverter : JsonConverter
	{
		public DBKeyStringConverter() { }
		public override bool CanConvert(Type objectType)
		{
			return true;
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			try
			{
				return new DBKey(reader.Value.ToString());
			}
			catch
			{
				return new DBKey(-1);
			}
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteValue(-1);
				return;
			}
			writer.WriteValue(value.ToString());
		}
	}
}
