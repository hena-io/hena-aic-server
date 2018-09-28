using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hena
{
	public static class JsonExtensions
	{
		public static string AsString(this JToken token) { return token.Value<string>(); }
		public static bool AsBool(this JToken token) { return token.Value<bool>(); }
		public static short AsShort(this JToken token) { return token.Value<short>(); }
		public static int AsInt(this JToken token) { return token.Value<int>(); }
		public static long AsLong(this JToken token) { return token.Value<long>(); }
		public static float AsFloat(this JToken token) { return token.Value<float>(); }
		public static double AsDouble(this JToken token) { return token.Value<double>(); }
		public static decimal AsDecimal(this JToken token) { return token.Value<decimal>(); }
		public static DBKey AsDBKey(this JToken token) { return token.AsLong(); }
		public static Uri AsUri(this JToken token) { return new Uri(token.AsString()); }
		public static TimeSpan AsTimeSpan(this JToken token) { return TimeSpan.Parse(token.AsString()); }
	}
}
