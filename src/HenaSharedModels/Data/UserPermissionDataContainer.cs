using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class UserPermissionDataContainer
		: DictionaryDataContainer<UserPermissionDataContainer, UserPermissionTypes, UserPermissionData>
	{

		protected override UserPermissionData NewValue(UserPermissionTypes key)
		{
			return new UserPermissionData() { PermissionType = key };
		}
		public bool CheckPermission(UserPermissionTypes permissionType, short level = 1)
		{
			var permission = Find(permissionType);
			if (permission == null)
				return false;

			return permission.CheckLevel(level);
		}

		public bool HasPermissions(params UserPermissionTypes[] permissionTypes)
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
	}
}
