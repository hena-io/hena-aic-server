using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_ad_unit
namespace Hena.DB
{
	// 광고 유닛 추가
	public class DBQuery_AdUnit_Insert : DBQuery<DBQuery_AdUnit_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_unit_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdUnitData Item = new AdUnitData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.UserId);
				parameters.Add(Item.AppId);
				parameters.Add(Item.AdUnitId);
				parameters.Add(Item.Name);
				parameters.Add(Item.AdDesignType);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 유닛 갱신
	public class DBQuery_AdUnit_Update : DBQuery<DBQuery_AdUnit_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_unit_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdUnitData Item = new AdUnitData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.AdUnitId);
				parameters.Add(Item.Name);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 유닛 삭제
	public class DBQuery_AdUnit_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_unit_delete";
	}

	// 광고 유닛 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_AdUnit_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_AdUnit_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AdUnitDataContainer Items { get; private set; } = new AdUnitDataContainer();

			public AdUnitData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AdUnitData();
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

	// 광고 유닛 조회( AdUnitId )
	public class DBQuery_AdUnit_Select : DBQuery_AdUnit_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_unit_select";
	}

	// 광고 유닛 조회( UserId )
	public class DBQuery_AdUnit_Select_By_UserId : DBQuery_AdUnit_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_unit_select_by_userid";
	}

	// 광고 유닛 조회( AppId )
	public class DBQuery_AdUnit_Select_By_AppId : DBQuery_AdUnit_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_unit_select_by_appid";
	}
}
