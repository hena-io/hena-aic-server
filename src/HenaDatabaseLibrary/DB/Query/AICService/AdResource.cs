using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_ad_resource
namespace Hena.DB
{
	// 광고 리소스 추가
	public class DBQuery_AdResource_Insert : DBQuery<DBQuery_AdResource_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_resource_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdResourceData Item = new AdResourceData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.UserId);
				parameters.Add(Item.AdResourceId);
				parameters.Add(Item.AdResourceType);
				parameters.Add(Item.ContentType);
				parameters.Add(Item.Width);
				parameters.Add(Item.Height);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 리소스 갱신
	public class DBQuery_AdResource_Update : DBQuery<DBQuery_AdResource_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_resource_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdResourceData Item = new AdResourceData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.AdResourceId);
				parameters.Add(Item.AdResourceType);
				parameters.Add(Item.Width);
				parameters.Add(Item.Height);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 리소스 삭제
	public class DBQuery_AdResource_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_resource_delete";
	}

	// 광고 리소스 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_AdResource_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_AdResource_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AdResourceDataContainer Items { get; private set; } = new AdResourceDataContainer();

			public AdResourceData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AdResourceData();
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

	// 광고 리소스 조회( AdResourceId )
	public class DBQuery_AdResource_Select : DBQuery_AdResource_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_resource_select";
	}

	// 광고 리소스 조회( UserId )
	public class DBQuery_AdResource_Select_By_UserId : DBQuery_AdResource_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_resource_select_by_userid";
	}
}
