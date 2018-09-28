using Newtonsoft.Json.Linq;

namespace Hena
{
	public interface IJSONSerializable
	{
		JToken ToJSON();
		bool FromJSON(JToken token);
	}
}
