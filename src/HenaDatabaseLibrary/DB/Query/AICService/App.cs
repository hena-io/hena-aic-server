using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_app
namespace Hena.DB
{
	// 앱 추가
	public class DBQuery_App_Insert : DBQuery<DBQuery_App_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_app_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AppData Item = new AppData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.UserId);
				parameters.Add(Item.AppId);
				parameters.Add(Item.Name);
				parameters.Add(Item.AppMarketType);
			}
		}
		#endregion // IN / OUT
	}

	// 앱 갱신
	public class DBQuery_App_Update : DBQuery<DBQuery_App_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_app_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AppData Item = new AppData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.AppId);
				parameters.Add(Item.Name);
			}
		}
		#endregion // IN / OUT
	}

	// 앱 삭제
	public class DBQuery_App_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_app_delete";
	}

	// 앱 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_App_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_App_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AppDataContainer Items { get; private set; } = new AppDataContainer();

			public AppData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AppData();
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

	// 앱 조회( AppId )
	public class DBQuery_App_Select : DBQuery_App_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_app_select";
	}

	// 앱 조회( UserId )
	public class DBQuery_App_Select_By_UserId : DBQuery_App_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_app_select_by_userid";
	}
}
