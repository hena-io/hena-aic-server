using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// revenue report
namespace Hena.DB
{
	// 마이닝 리포트 조회( OUT 선 정의 )
	public abstract class DBQuery_MiningReport_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_MiningReport_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public MiningReportDataContainer Items { get; private set; } = new MiningReportDataContainer();

			public MiningReportData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new MiningReportData();
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

	// 마이닝 리포트 조회( UserId )
	public class DBQuery_MiningReport_Select_By_UserId : DBQuery_MiningReport_Select_Base<DBQuery_MiningReport_Select_By_UserId.IN_DATA>
	{
		public override string ProcedureName => "sp_mining_report_select_by_userid";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey UserId = GlobalDefine.INVALID_DBKEY;

			// Local Time
			public DateTime BeginTimeLocal = DateTime.MinValue;
			public DateTime EndTimeLocal = DateTime.MinValue;

			public TimeSpan TimeOffset = TimeSpan.Zero;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(UserId);
				parameters.Add(BeginTimeLocal);
				parameters.Add(EndTimeLocal);
				parameters.Add(TimeOffset);
			}
		}
		#endregion // IN / OUT
	}
}
