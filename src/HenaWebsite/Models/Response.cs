using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HenaWebsite.Models
{
	public interface IResponseData
	{

	}

	public class ResponseBase
	{
		// 결과
		[JsonConverter(typeof(StringEnumConverter))]
		public ErrorCode Result { get; set; }

		// 메시지
		public string Message { get; set; } = string.Empty;
	}

	public class DataResponse : ResponseBase
	{
		// 데이터
		public IResponseData Data { get; set; }
	}

	public class JSONResponse : ResponseBase
	{
		// 데이터
		public JToken Data { get; set; }
	}

	public class Request
	{
	}
}
