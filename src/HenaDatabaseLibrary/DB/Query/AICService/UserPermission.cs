using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_user_permission
namespace Hena.DB
{
	// 유저 권한 추가
	public class DBQuery_User_Permission_Insert : DBQuery<DBQuery_User_Permission_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_permission_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserPermissionData PermissionData { get; private set; } = new UserPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.UserId);
				parameters.Add(PermissionData.PermissionType);
				parameters.Add(PermissionData.Level);
				parameters.Add(PermissionData.RegisterTime);
			}
		}
		#endregion // IN / OUT
	}

	// 유저 권한 조회( OUT 선 정의 )
	public abstract class DBQuery_User_Permission_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_User_Permission_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public UserPermissionDataContainer Items { get; private set; } = new UserPermissionDataContainer();

			public bool FromDataTable(DataTable table)
			{
				try
				{
					Items.Clear();
					foreach (DataRow row in table.Rows)
					{
						var item = new UserPermissionData();
						item.FromDBTable(row);
						if (item.PermissionType == UserPermissionTypes.None)
							continue;

						Items.Add(item.PermissionType, item);
					}
					return true;

				}
				catch
				{
					return false;
				}
			}
		}
		#endregion // IN / OUT
	}

    // 유저권한 조회
    public class DBQuery_User_Permission_Select
        : DBQuery_User_Permission_Select_Base<DBQuery_User_Permission_Select.IN_DATA>
    {
        public override string ProcedureName => "sp_user_permission_select";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;
            public UserPermissionTypes PermissionType { get; set; } = UserPermissionTypes.None;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(UserId);
                parameters.Add(PermissionType);
            }
        }
        #endregion // IN / OUT
    }

    // 유저권한 조회( UserId )
    public class DBQuery_User_Permission_Select_By_UserId : DBQuery_User_Permission_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_user_permission_select_by_userid";
	}

	// 유저권한 조회( PermissionType )
	public class DBQuery_User_Permission_Select_By_PermissionType 
		: DBQuery<DBQuery_User_Permission_Select_By_PermissionType.IN_DATA, DBQuery_User_Permission_Select_By_PermissionType.OUT_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_permission_select_by_permissiontype";
		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserPermissionTypes PermissionType { get; set; } = UserPermissionTypes.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionType);
			}
		}

		public class OUT_DATA : IOUT
		{
			public List<UserPermissionData> Items { get; private set; } = new List<UserPermissionData>();

			public bool FromDataTable(DataTable table)
			{
				try
				{
					Items.Clear();
					foreach (DataRow row in table.Rows)
					{
						var item = new UserPermissionData();
						item.FromDBTable(row);
						if (item.PermissionType == UserPermissionTypes.None)
							continue;

						Items.Add(item);
					}
					return true;

				}
				catch
				{
					return false;
				}
			}
		}
		#endregion // IN / OUT

		public static async Task<UserPermissionData[]> FromDBByPermissionTypeAsync(UserPermissionTypes permissionType)
		{
			var query = new DBQuery_User_Permission_Select_By_PermissionType();
			query.IN.PermissionType = permissionType;
			var result = await DBThread.Instance.ReqQueryAsync(query);
			return query.OUT.Items.ToArray();
		}
	}

	// 유저 권한 갱신 - 레벨
	public class DBQuery_User_Permission_Level_Update : DBQuery<DBQuery_User_Permission_Level_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_permission_level_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserPermissionData PermissionData { get; set; } = new UserPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.UserId);
				parameters.Add(PermissionData.PermissionType);
				parameters.Add(PermissionData.Level);
			}
		}
		#endregion // IN / OUT
	}

	// 유저 권한 삭제
	public class DBQuery_User_Permission_Delete : DBQuery<DBQuery_User_Permission_Delete.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_permission_delete";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			// 유저 DB키
			public DBKey UserId = GlobalDefine.INVALID_DBKEY;

			// 권한 타입
			public UserPermissionTypes PermissionType { get; set; } = UserPermissionTypes.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserId);
				parameters.Add(PermissionType);
			}
		}
		#endregion // IN / OUT
	}
}
