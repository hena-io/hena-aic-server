using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

namespace Hena.DB
{
	#region tbl_user
	// 유저 추가
	public class DBQuery_User_Insert : DBQuery<DBQuery_User_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserBasicData BasicData = new UserBasicData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(BasicData.UserDBKey);
				parameters.Add(BasicData.EMail);
				parameters.Add(BasicData.Password);
			}
		}
		#endregion // IN / OUT
	}
	
    // 유저 비밀번호 변경
    public class DBQuery_User_Update_Password : DBQuery<DBQuery_User_Update_Password.IN_DATA>
    {
        public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_user_update_password";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            // 유저 DB키
            public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

            // 이메일 알림 서비스 이용여부
            public string Password = string.Empty;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(UserDBKey);
                parameters.Add(Password);
            }
        }
        #endregion // IN / OUT
    }

    // 유저 정보 조회( OUT 선 정의 )
    public abstract class DBQuery_User_Select_Base<T_IN> 
		: DBQuery<T_IN, DBQuery_User_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public UserBasicDataContainer Items { get; private set; } = new UserBasicDataContainer();

            public UserBasicData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new UserBasicData();
						item.FromDBTable(row);
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
	}

	// 유저정보 조회( UserDBKey )
	public class DBQuery_User_Select_By_UserDBKey : DBQuery_User_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_user_select_by_user_dbkey";
	}

	// 유저정보 조회( EMail )
	public class DBQuery_User_Select_By_EMail : DBQuery_User_Select_Base<COMMON_IN_DATA_EMailOnly>
	{
		public override string ProcedureName => "sp_user_select_by_email";
	}

	// 유저정보 조회( Username )
	public class DBQuery_User_Select_By_UserName : DBQuery_User_Select_Base<COMMON_IN_DATA_UserNameOnly>
	{
		public override string ProcedureName => "sp_user_select_by_username";
	}

	// 유저정보 조회( Like EMail )
	public class DBQuery_User_Select_By_LikeEMail 
        : DBQuery_User_Select_Base<DBQuery_User_Select_By_LikeEMail.IN_DATA>
    {
        public override string ProcedureName => "sp_user_select_by_like_email";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public string EMail = string.Empty;
            public int Offset = 0;
            public int Limit = 1;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(EMail);
                parameters.Add(Offset);
                parameters.Add(Limit);
            }
        }
        #endregion // IN / OUT
    }

    // 유저정보 갯수 조회( Like EMail )
    public class DBQuery_User_Select_By_LikeEMail_Count
        : DBQuery<DBQuery_User_Select_By_LikeEMail_Count.IN_DATA, COMMON_OUT_DATA_CountOnly>
    {
		public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_user_select_by_like_email_count";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public string EMail = string.Empty;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(EMail);
            }
        }
        #endregion // IN / OUT
    }

    // 유저정보 조회( 생성일 기준 정렬 )
    public class DBQuery_User_Select_By_CreateTime
        : DBQuery_User_Select_Base<DBQuery_User_Select_By_CreateTime.IN_DATA>
    {
        public override string ProcedureName => "sp_user_select_by_createtime";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public DateTime BeginCreateTime = DateTime.MinValue;
            public DateTime EndCreateTime = DateTime.UtcNow;
            public bool SortByCreateTimeDesc = false;
            public int Offset = 0;
            public int Limit = 1;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(BeginCreateTime);
                parameters.Add(EndCreateTime);
                parameters.Add(SortByCreateTimeDesc);
                parameters.Add(Offset);
                parameters.Add(Limit);
            }
        }
        #endregion // IN / OUT
    }

    // 유저갯수 조회
    public class DBQuery_User_Select_Count
        : DBQuery<DBQuery_User_Select_Count.IN_DATA, COMMON_OUT_DATA_CountOnly>
    {
        public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_user_select_count";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public override void FillParameters(List<object> parameters)
            {
            }
        }
        #endregion // IN / OUT

        public static async Task<int> GetAccountCountFromDBAsync()
        {
            DBQuery_User_Select_Count query = new DBQuery_User_Select_Count();
            await DBThread.Instance.ReqQueryAsync(query);
            return query.OUT.Count;
        }
    }
	#endregion // tbl_user

	#region tbl_user_permission
	// 유저 권한 추가
	public class DBQuery_User_Permission_Insert : DBQuery<DBQuery_User_Permission_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_permission_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserPermissionData PermissionData = new UserPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.UserDBKey);
				parameters.Add(PermissionData.PermissionType.ToString());
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
						if (item.PermissionType == AccountPermissionType.None)
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
            public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;
            public AccountPermissionType PermissionType = AccountPermissionType.None;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(UserDBKey);
                parameters.Add(PermissionType.ToString());
            }
        }
        #endregion // IN / OUT
    }

    // 유저권한 조회( UserDBKey )
    public class DBQuery_User_Permission_Select_By_UserDBKey : DBQuery_User_Permission_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_user_permission_select_by_userdbkey";
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
			public AccountPermissionType PermissionType = AccountPermissionType.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionType.ToString());
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
						if (item.PermissionType == AccountPermissionType.None)
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

		public static async Task<UserPermissionData[]> FromDBByPermissionTypeAsync(AccountPermissionType permissionType)
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
			public UserPermissionData PermissionData = new UserPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.UserDBKey);
				parameters.Add(PermissionData.PermissionType.ToString());
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
			public DBKey UserDBKey = GlobalDefine.INVALID_DBKEY;

			// 권한 타입
			public AccountPermissionType PermissionType = AccountPermissionType.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserDBKey);
				parameters.Add(PermissionType.ToString());
			}
		}
		#endregion // IN / OUT
	}
	#endregion // tbl_user_permission
}
