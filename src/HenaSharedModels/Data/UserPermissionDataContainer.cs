using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class UserPermissionDataContainer
		: DictionaryDataContainer<UserPermissionDataContainer, AccountPermissionType, UserPermissionData>
	{

		protected override UserPermissionData NewValue(AccountPermissionType key)
		{
			return new UserPermissionData() { PermissionType = key };
		}
		public bool CheckPermission(AccountPermissionType permissionType, short level = 0)
		{
			var permission = Find(permissionType);
			if (permission == null)
				return false;

			return permission.CheckLevel(level);
		}

		public bool HasPermissions(params AccountPermissionType[] permissionTypes)
		{
			if (permissionTypes.Length == 0)
				return false;

			foreach (var it in permissionTypes)
			{
				if (ContainsKey(it) == false)
					return false;
			}
			return true;

		}
		protected override bool FromJSON_Key(JToken token, out AccountPermissionType outKey)
		{
			return token.AsEnum(out outKey, true);
		}

		protected override JToken ToJSON_Key(AccountPermissionType key)
		{
			return new JValue(key.ToString());
		}
	}
}
