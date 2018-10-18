using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_balance
namespace Hena.DB
{
	// 잔고 더하기
	public class DBQuery_Balance_Add : DBQuery<DBQuery_Balance_Add.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_balance_add";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey UserId = GlobalDefine.INVALID_DBKEY;
			public CurrencyTypes CurrencyType = CurrencyTypes.None;
			public decimal AddBalance = 0m;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserId);
				parameters.Add(CurrencyType);
				parameters.Add(AddBalance);
			}
		}
		#endregion // IN / OUT
	}

	// 잔고 갱신
	public class DBQuery_Balance_Update : DBQuery<DBQuery_Balance_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_balance_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey UserId = GlobalDefine.INVALID_DBKEY;
			public CurrencyTypes CurrencyType = CurrencyTypes.None;
			public decimal Balance = 0m;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserId);
				parameters.Add(CurrencyType);
				parameters.Add(Balance);
			}
		}
		#endregion // IN / OUT
	}

	// 잔고 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_Balance_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_Balance_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public BalanceDataContainer Items { get; private set; } = new BalanceDataContainer();

			public BalanceData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new BalanceData();
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

	// 잔고 조회( UserId, CurrencyType )
	public class DBQuery_Balance_Select : DBQuery_Balance_Select_Base<DBQuery_Balance_Select.IN_DATA>
	{
		public override string ProcedureName => "sp_balance_select";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey UserId = GlobalDefine.INVALID_DBKEY;
			public CurrencyTypes CurrencyType = CurrencyTypes.None;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserId);
				parameters.Add(CurrencyType);
			}
		}
		#endregion // IN / OUT
	}

	// 잔고 조회( UserId )
	public class DBQuery_Balance_Select_By_UserId : DBQuery_Balance_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_balance_select_by_userid";
	}
}
