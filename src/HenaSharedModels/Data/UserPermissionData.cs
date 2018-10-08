using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	// 계정 권한
	public class UserPermissionData
	{
		// 기본값
		private readonly static UserPermissionData Default = new UserPermissionData();

		// 계정 DB키
		public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

		// 권한 타입
		public UserPermissionTypes PermissionType { get; set; } = UserPermissionTypes.None;

		// 권한 레벨
		public short Level { get; set; } = 0;

		// 권한 등록일
		public DateTime RegisterTime { get; set; } = GlobalDefine.INVALID_DATETIME;

		public bool CheckLevel(short level = 0)
		{
			return Level >= level;
		}
	}
}
