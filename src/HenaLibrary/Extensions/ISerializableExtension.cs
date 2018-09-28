using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class SerializableExtension
	{
		public static string ToBase64<T>(T target)
			where T : ISerializable
		{
			if (target == null)
				return "null";

			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, target);
			var base64 = Convert.ToBase64String(stream.ToArray());
            stream.Close();
            return base64;
		}

		public static T FromBase64<T>(string base64) 
			where T : ISerializable
		{
			if (base64 == "null")
				return default(T);

			MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64));
			BinaryFormatter formatter = new BinaryFormatter();
			var result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
		}
	}
}
