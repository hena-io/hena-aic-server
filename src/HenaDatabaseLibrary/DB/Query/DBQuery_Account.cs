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
	#region table_account
	// 계정 추가
	public class DBQuery_Account_Insert : DBQuery<DBQuery_Account_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AccountBasicData BasicData = new AccountBasicData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(BasicData.AccountDBKey);
				parameters.Add(BasicData.CreateTime);
				parameters.Add(BasicData.GivenName);
				parameters.Add(BasicData.SurName);
				parameters.Add(BasicData.Username);
				parameters.Add(BasicData.EMail);
				parameters.Add(BasicData.Password);
			}
		}
		#endregion // IN / OUT
	}
	
    // 계정 비밀번호 변경
    public class DBQuery_Account_Update_Password : DBQuery<DBQuery_Account_Update_Password.IN_DATA>
    {
        public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_account_update_password";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            // 계정 DB키
            public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;

            // 이메일 알림 서비스 이용여부
            public string Password = string.Empty;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(AccountDBKey);
                parameters.Add(Password);
            }
        }
        #endregion // IN / OUT
    }

    // 계정 정보 조회( OUT 선 정의 )
    public abstract class DBQuery_Account_Select_Base<T_IN> 
		: DBQuery<T_IN, DBQuery_Account_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AccountBasicDataContainer Items { get; private set; } = new AccountBasicDataContainer();

            public AccountBasicData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AccountBasicData();
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

	// 계정정보 조회( AccountDBKey )
	public class DBQuery_Account_Select_By_AccountDBKey : DBQuery_Account_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_account_select_by_account_dbkey";
	}

	// 계정정보 조회( EMail )
	public class DBQuery_Account_Select_By_EMail : DBQuery_Account_Select_Base<COMMON_IN_DATA_EMailOnly>
	{
		public override string ProcedureName => "sp_account_select_by_email";
	}

	// 계정정보 조회( Username )
	public class DBQuery_Account_Select_By_UserName : DBQuery_Account_Select_Base<COMMON_IN_DATA_UserNameOnly>
	{
		public override string ProcedureName => "sp_account_select_by_username";
	}

	// 계정정보 조회( Like EMail )
	public class DBQuery_Account_Select_By_LikeEMail 
        : DBQuery_Account_Select_Base<DBQuery_Account_Select_By_LikeEMail.IN_DATA>
    {
        public override string ProcedureName => "sp_account_select_by_like_email";

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

    // 계정정보 갯수 조회( Like EMail )
    public class DBQuery_Account_Select_By_LikeEMail_Count
        : DBQuery<DBQuery_Account_Select_By_LikeEMail_Count.IN_DATA, COMMON_OUT_DATA_CountOnly>
    {
		public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_account_select_by_like_email_count";

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

    // 계정정보 조회( 생성일 기준 정렬 )
    public class DBQuery_Account_Select_By_CreateTime
        : DBQuery_Account_Select_Base<DBQuery_Account_Select_By_CreateTime.IN_DATA>
    {
        public override string ProcedureName => "sp_account_select_by_createtime";

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

    // 계정갯수 조회
    public class DBQuery_Account_Select_Count
        : DBQuery<DBQuery_Account_Select_Count.IN_DATA, COMMON_OUT_DATA_CountOnly>
    {
        public override DBType DBType => DBType.Hena_AIC_Service;
        public override string ProcedureName => "sp_account_select_count";

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
            DBQuery_Account_Select_Count query = new DBQuery_Account_Select_Count();
            await DBThread.Instance.ReqQueryAsync(query);
            return query.OUT.Count;
        }
    }
	#endregion // table_account

	#region table_login_history
	// 계정 기록 추가
	public class DBQuery_Account_History_Insert : DBQuery<DBQuery_Account_History_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_history_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;
			public string UserAgent = string.Empty;
			public string Comment = string.Empty;
			public string IPAddress = string.Empty;
			public DateTime HistoryTime = DateTime.UtcNow;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(AccountDBKey);
				parameters.Add(UserAgent);
				parameters.Add(Comment);
				parameters.Add(IPAddress);
				parameters.Add(HistoryTime);
			}
		}
		#endregion // IN / OUT

		public static void LogHistory(DBKey accountDBKey, string userAgent, string comment, string ipAddress, DateTime historyTime)
		{
			Task.Run(async () =>
			{
				await LogHistoryAsync(accountDBKey, userAgent, comment, ipAddress, historyTime);
			});
		}
		public static async Task LogHistoryAsync(DBKey accountDBKey, string userAgent, string comment, string ipAddress, DateTime historyTime)
		{
			DBQuery_Account_History_Insert query = new DBQuery_Account_History_Insert();
			query.IN.AccountDBKey = accountDBKey;
			query.IN.UserAgent = userAgent;
			query.IN.Comment = comment;
			query.IN.IPAddress = ipAddress;
			query.IN.HistoryTime = historyTime;
			await DBThread.Instance.ReqQueryAsync(query);
		}
	}
	#endregion // table_login_history

	#region table_account_permission
	// 계정 권한 추가
	public class DBQuery_Account_Permission_Insert : DBQuery<DBQuery_Account_Permission_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_permission_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AccountPermissionData PermissionData = new AccountPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.AccountDBKey);
				parameters.Add(PermissionData.PermissionType.ToString());
				parameters.Add(PermissionData.Level);
				parameters.Add(PermissionData.RegisterTime);
			}
		}
		#endregion // IN / OUT
	}

	// 계정 권한 조회( OUT 선 정의 )
	public abstract class DBQuery_Account_Permission_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_Account_Permission_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AccountPermissionDataContainer Items { get; private set; } = new AccountPermissionDataContainer();

			public bool FromDataTable(DataTable table)
			{
				try
				{
					Items.Clear();
					foreach (DataRow row in table.Rows)
					{
						var item = new AccountPermissionData();
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

    // 계정권한 조회
    public class DBQuery_Account_Permission_Select
        : DBQuery_Account_Permission_Select_Base<DBQuery_Account_Permission_Select.IN_DATA>
    {
        public override string ProcedureName => "sp_account_permission_select";

        #region IN / OUT
        public class IN_DATA : IN_BASE
        {
            public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;
            public AccountPermissionType PermissionType = AccountPermissionType.None;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(AccountDBKey);
                parameters.Add(PermissionType.ToString());
            }
        }
        #endregion // IN / OUT
    }

    // 계정권한 조회( AccountDBKey )
    public class DBQuery_Account_Permission_Select_By_AccountDBKey : DBQuery_Account_Permission_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_account_permission_select_by_accountdbkey";
	}

	// 계정권한 조회( PermissionType )
	public class DBQuery_Account_Permission_Select_By_PermissionType 
		: DBQuery<DBQuery_Account_Permission_Select_By_PermissionType.IN_DATA, DBQuery_Account_Permission_Select_By_PermissionType.OUT_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_permission_select_by_permissiontype";
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
			public List<AccountPermissionData> Items { get; private set; } = new List<AccountPermissionData>();

			public bool FromDataTable(DataTable table)
			{
				try
				{
					Items.Clear();
					foreach (DataRow row in table.Rows)
					{
						var item = new AccountPermissionData();
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

		public static async Task<AccountPermissionData[]> FromDBByPermissionTypeAsync(AccountPermissionType permissionType)
		{
			var query = new DBQuery_Account_Permission_Select_By_PermissionType();
			query.IN.PermissionType = permissionType;
			var result = await DBThread.Instance.ReqQueryAsync(query);
			return query.OUT.Items.ToArray();
		}
	}

	// 계정 권한 갱신 - 레벨
	public class DBQuery_Account_Permission_Level_Update : DBQuery<DBQuery_Account_Permission_Level_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_permission_level_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AccountPermissionData PermissionData = new AccountPermissionData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(PermissionData.AccountDBKey);
				parameters.Add(PermissionData.PermissionType.ToString());
				parameters.Add(PermissionData.Level);
			}
		}
		#endregion // IN / OUT
	}

	// 계정 권한 삭제
	public class DBQuery_Account_Permission_Delete : DBQuery<DBQuery_Account_Permission_Delete.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_account_permission_delete";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			// 계정 DB키
			public DBKey AccountDBKey = GlobalDefine.INVALID_DBKEY;

			// 권한 타입
			public AccountPermissionType PermissionType = AccountPermissionType.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(AccountDBKey);
				parameters.Add(PermissionType.ToString());
			}
		}
		#endregion // IN / OUT
	}
	#endregion // table_account_permission
}
