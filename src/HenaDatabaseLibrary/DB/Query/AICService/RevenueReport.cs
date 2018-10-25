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
	// 수익 리포트 조회( OUT 선 정의 )
	public abstract class DBQuery_RevenueReport_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_RevenueReport_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public RevenueReportDataContainer Items { get; private set; } = new RevenueReportDataContainer();

			public RevenueReportData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new RevenueReportData();
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

	// 수익 리포트 조회( Publisher - UserId )
	public class DBQuery_RevenueReport_Select_By_PublisherId : DBQuery_RevenueReport_Select_Base<DBQuery_RevenueReport_Select_By_PublisherId.IN_DATA>
	{
		public override string ProcedureName => "sp_revenue_report_select_by_publisherid";

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

	// 수익 리포트 조회( Customer - UserId )
	public class DBQuery_RevenueReport_Select_By_CustomerId : DBQuery_RevenueReport_Select_Base<DBQuery_RevenueReport_Select_By_CustomerId.IN_DATA>
	{
		public override string ProcedureName => "sp_revenue_report_select_by_customerid";

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

	// 수익 리포트 조회( AdvertiserId - UserId )
	public class DBQuery_RevenueReport_Select_By_AdvertiserId : DBQuery_RevenueReport_Select_Base<DBQuery_RevenueReport_Select_By_AdvertiserId.IN_DATA>
	{
		public override string ProcedureName => "sp_revenue_report_select_by_advertiserid";

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
