using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Hena
{
    public class WebConfiguration : Singleton<WebConfiguration>
        , IJSONSerializable
        , IFileSerializable
    {
        private static WebConfiguration Default { get; set; } = new WebConfiguration();

        public string ROOT_DIRECTORY { get; private set; } = string.Empty;

        public const string ServerConfigFileName = "config.json";
        public string ServerConfigFilePath { get { return CombinePath(ROOT_DIRECTORY, ServerConfigFileName); } }

        // DB 설정파일 경로
        public string DatabaseConfigFileName { get; set; } = "database.json";
        public string DatabaseConfigFilePath { get { return CombinePath(ROOT_DIRECTORY, DatabaseConfigFileName); } }

		#region EMail
		// 이메일 SMTP 호스트
		public string SupportEmailSmtpHost { get; set; } = "smtp.gmail.com";

		// 이메일 SMTP 포트
		public short SupportEmailSmtpPort { get; set; } = 465;	// 587

		// 이메일 ID
		public string SupportEmailId { get; set; } = string.Empty;

        // 이메일 비밀번호
        public string SupportEmailPassword { get; set; } = string.Empty;

        // 이메일 앞에 표시될 이름.
        public string SupportEmailDisplayName { get; set; } = string.Empty;
		#endregion  // EMail

		#region FTP
		public string FTPHost { get; set; } = "";
		public string FTPId { get; set; } = "";
		public string FTPPassword { get; set; } = "";
		#endregion // FTP


		private static string CombinePath(string directory, string filename)
		{
			return Path.GetFullPath(directory + "/" + filename);
		}

		// 초기화
		protected override void OnInitialize()
		{
            ROOT_DIRECTORY = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

            if (LoadFromFile(ServerConfigFilePath) == false)
			{
				SaveToFile(ServerConfigFilePath);
			}
		}

		// 해제
		protected override void OnRelease()
		{

		}


		#region IFileSerializable
		public bool LoadFromFile(string filePath)
		{
			return IFileSerializableExtension.LoadFromFile(this, filePath);
		}

		public bool SaveToFile(string filePath)
		{
			return IFileSerializableExtension.SaveToFile(this, filePath);
		}
		#endregion // IFileSerializable

		#region IJSONSerializable
		public bool FromJSON(JToken token)
		{
			DatabaseConfigFileName = JSONUtility.GetValue(token, "DatabaseConfigFileName", Default.DatabaseConfigFileName);

			SupportEmailSmtpHost = JSONUtility.GetValue(token, "SupportEmailSmtpHost", Default.SupportEmailSmtpHost);
			SupportEmailSmtpPort = JSONUtility.GetValue(token, "SupportEmailSmtpPort", Default.SupportEmailSmtpPort);
            SupportEmailId = JSONUtility.GetValue(token, "SupportEmailId", Default.SupportEmailId);
            SupportEmailPassword = JSONUtility.GetValue(token, "SupportEmailPassword", Default.SupportEmailPassword);
            SupportEmailDisplayName = JSONUtility.GetValue(token, "SupportEmailDisplayName", Default.SupportEmailDisplayName);
            
            FTPHost = JSONUtility.GetValue(token, "FTPHost", Default.FTPHost);
            FTPId = JSONUtility.GetValue(token, "FTPId", Default.FTPId);
            FTPPassword = JSONUtility.GetValue(token, "FTPPassword", Default.FTPPassword);
            return true;
		}

		public JToken ToJSON()
		{
			JObject jObject = new JObject();
			jObject["DatabaseConfigFileName"] = DatabaseConfigFileName;

            jObject["SupportEmailSmtpHost"] = SupportEmailSmtpHost;
            jObject["SupportEmailSmtpPort"] = SupportEmailSmtpPort;
            jObject["SupportEmailId"] = SupportEmailId;
            jObject["SupportEmailPassword"] = SupportEmailPassword;
            jObject["SupportEmailDisplayName"] = SupportEmailDisplayName;

			jObject["FTPHost"] = FTPHost;
			jObject["FTPId"] = FTPId;
			jObject["FTPPassword"] = FTPPassword;

			return jObject;
		}
		#endregion // IJSONSerializable
	}
}
