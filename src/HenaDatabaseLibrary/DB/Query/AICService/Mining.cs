using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_mining_history
namespace Hena.DB
{
	// 마이닝 기록 추가
	public class DBQuery_MiningHistory_Insert : DBQuery<DBQuery_MiningHistory_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_mining_history_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public MiningHistoryData Item = new MiningHistoryData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.MiningHistoryId);
				parameters.Add(Item.UserId);
				parameters.Add(Item.CurrencyType);
				parameters.Add(Item.MiningAmount);
			}
		}
		#endregion // IN / OUT
	}

	// 마이닝 기록 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_MiningHistory_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_MiningHistory_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public MiningHistoryDataContainer Items { get; private set; } = new MiningHistoryDataContainer();

			public MiningHistoryData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new MiningHistoryData();
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

	// 마이닝 기록 조회( MiningHistoryId )
	public class DBQuery_MiningHistory_Select : DBQuery_MiningHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_mining_history_select";
	}

	// 마이닝 기록 조회( UserId )
	public class DBQuery_MiningHistory_Select_By_UserId : DBQuery_MiningHistory_Select_Base<COMMON_IN_DATA_DBKeyOffsetLimit>
	{
		public override string ProcedureName => "sp_mining_history_select_by_userid";
	}
}
