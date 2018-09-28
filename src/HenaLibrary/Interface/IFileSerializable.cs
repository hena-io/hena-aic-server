using Newtonsoft.Json.Linq;
using System.IO;

namespace Hena
{
	public interface IFileSerializable
	{
		bool LoadFromFile(string filePath);
		bool SaveToFile(string filePath);
	}

	public static class IFileSerializableExtension
	{
		public static bool LoadFromFile<T>(this T item, string filePath, bool isEncrypted = false, string password = "") where T : IFileSerializable, IJSONSerializable
		{
			if (File.Exists(filePath) == false)
				return false;

			string jsonText = File.ReadAllText(filePath);
            if(isEncrypted)
            {
                jsonText = AESUtility.Decrypt(jsonText, password);
            }
            var jToken = JToken.Parse(jsonText);
			return item.FromJSON(jToken);
		}

        public static bool SaveToFile<T>(this T item, string filePath, bool isEncrypt = false, string password = "") where T : IFileSerializable, IJSONSerializable
		{
			Utility.CreateDirectory(filePath);
			var jToken = item.ToJSON();
			if (jToken == null)
				return false;

			string jsonText = jToken.ToString(Newtonsoft.Json.Formatting.Indented);
            if( isEncrypt )
            {
                jsonText = AESUtility.Encrypt(jsonText, password);
            }
			File.WriteAllText(filePath, jsonText);
			return true;
		}        
    }
}
