using Hena;
using Hena.Library.Extensions;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HenaTestCode.HenaLibrary
{
	public static class HenaLibrary
	{
		public class ClientId
		{
			// 광고 유닛 ID
			public DBKey AdUnitId { get; set; } = GlobalDefine.INVALID_DBKEY;

			// 광고 디자인 ID
			public DBKey AdDesignId { get; set; } = GlobalDefine.INVALID_DBKEY;

			// 광고 디자인 타입
			public AdDesignTypes.en AdDesignType { get; set; } = AdDesignTypes.en.None;

			public ClientId() { }
			public ClientId(string source)
			{
				this.Decode(source);
			}
		}

		public static async Task Run()
		{
			ClientId cid = new ClientId();
			cid.AdUnitId = 987654321;
			cid.AdDesignId = 30650213543;
			var encoded = cid.Encode();
			var decoded = ObjectExtension.Decode<ClientId>(encoded);
			//FtpClient ftpClient = new FtpClient("ftp://hena.io", "henacoin", "henacoin2");
			//await ftpClient.UploadAsync("tttt/testfile.txt", "test12341231".ToBytes());
			await Task.Yield();
		}
	}
}
