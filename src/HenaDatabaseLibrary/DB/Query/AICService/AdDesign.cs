using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_ad_design
namespace Hena.DB
{
	// 광고 디자인 추가
	public class DBQuery_AdDesign_Insert : DBQuery<DBQuery_AdDesign_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_design_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdDesignData Item = new AdDesignData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.UserId);
				parameters.Add(Item.CampaignId);
				parameters.Add(Item.AdDesignId);
				parameters.Add(Item.Name);
				parameters.Add(Item.AdDesignType);
				parameters.Add(Item.ResourceName);
				parameters.Add(Item.DestinationUrl);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 디자인 갱신
	public class DBQuery_AdDesign_Update : DBQuery<DBQuery_AdDesign_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_design_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdDesignData Item = new AdDesignData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.AdDesignId);
				parameters.Add(Item.Name);
				parameters.Add(Item.AdDesignType);
				parameters.Add(Item.ResourceName);
				parameters.Add(Item.DestinationUrl);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 디자인 삭제
	public class DBQuery_AdDesign_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_ad_design_delete";
	}

	// 광고 디자인 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_AdDesign_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_AdDesign_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AdDesignDataContainer Items { get; private set; } = new AdDesignDataContainer();

			public AdDesignData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AdDesignData();
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

	// 광고 디자인 조회( AdDesignId )
	public class DBQuery_AdDesign_Select : DBQuery_AdDesign_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_design_select";
	}

	// 광고 디자인 조회( UserId )
	public class DBQuery_AdDesign_Select_By_UserId : DBQuery_AdDesign_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_design_select_by_userid";
	}

	// 광고 디자인 조회( CampaignId )
	public class DBQuery_AdDesign_Select_By_CampaignId: DBQuery_AdDesign_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_design_select_by_campaignid";
	}
}
