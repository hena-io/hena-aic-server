using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_user
namespace Hena.DB
{
	// 유저 추가
	public class DBQuery_User_Insert : DBQuery<DBQuery_User_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_user_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public UserBasicData BasicData { get; private set; } = new UserBasicData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(BasicData.UserId);
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
            public DBKey UserId { get; set; } = GlobalDefine.INVALID_DBKEY;

            // 이메일 알림 서비스 이용여부
            public string Password { get; set; } = string.Empty;

            public override void FillParameters(List<object> parameters)
            {
                parameters.Add(UserId);
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

	// 유저정보 조회( UserId )
	public class DBQuery_User_Select : DBQuery_User_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_user_select";
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
}
