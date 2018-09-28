using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.DB
{
	public class DBConnectionData : IJSONSerializable, ICloneable<DBConnectionData>
	{
		private static DBConnectionData Default = new DBConnectionData();

		// 접속 URL
		public string Host { get; set; } = string.Empty;
		
		// 접속 포트
		public int Port { get; set; } = 3306;

		// 데이터베이스 이름
		public string Database { get; set; } = string.Empty;

		// 유저 아이디
		public string ID { get; set; } = string.Empty;

		// 유저 비밀번호
		public string Password { get; set; } = string.Empty;

		// 접속 문자열 조합
		public string ConnectionString
		{
			get
			{
				StringBuilder conn = new StringBuilder(1024);
				conn.Append($"Server={Host};");
				conn.Append($"Port={Port};");
				conn.Append($"Database={Database};");
				conn.Append($"Uid={ID};");
				conn.Append($"Pwd={Password};");
				conn.Append($"SslMode=none;");
				conn.Append($"Charset=utf8mb4;");
				conn.Append($"persistsecurityinfo=true;");
				conn.Append($"allowPublicKeyRetrieval=true;");
				return conn.ToString();
			}
		}

		#region ICloneable
		public DBConnectionData Clone()
		{
			return this.Clone<DBConnectionData>();
		}

		public void CopyTo(ref DBConnectionData target)
		{
			target.Host = Host;
			target.Port = Port;
			target.Database = Database;
			target.ID = ID;
			target.Password = Password;
		}
		#endregion // ICloneable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			Host = JSONUtility.GetValue(token, "host", Default.Host);
			Port = JSONUtility.GetValue(token, "port", Default.Port);
			Database = JSONUtility.GetValue(token, "database", Default.Database);
			ID = JSONUtility.GetValue(token, "id", Default.ID);
			Password = JSONUtility.GetValue(token, "password", Default.Password);
			return true;
		}

		public JToken ToJSON()
		{
			var jObject = new JObject();
			jObject["host"] = Host;
			jObject["port"] = Port;
			jObject["database"] = Database;
			jObject["id"] = ID;
			jObject["password"] = Password;
			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
