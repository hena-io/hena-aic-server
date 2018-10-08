using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hena.Library.Extensions;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 전체 데이터
	public class UserData 
	{
		// 기본값
		private readonly static UserData Default = new UserData();

		// 계정 기본정보
		public UserBasicData BasicData { get; set; } = new UserBasicData();

		// 계정 권한
		public UserPermissionDataContainer Permissions { get; set; } = new UserPermissionDataContainer();

        // 유저 타임존 Offset
        public TimeSpan TimeZoneOffset { get; set; } = TimeSpan.Zero;

        public object CustomData { get; set; }
	}
}
