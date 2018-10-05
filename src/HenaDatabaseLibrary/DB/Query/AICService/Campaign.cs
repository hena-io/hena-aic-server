using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;

// tbl_campaign
namespace Hena.DB
{
	// 캠페인 추가
	public class DBQuery_Campaign_Insert : DBQuery<DBQuery_Campaign_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_campaign_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public CampaignData Item = new CampaignData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.UserDBKey);
				parameters.Add(Item.CampaignDBKey);
				parameters.Add(Item.Name);
				parameters.Add(Item.CampaignType);
				parameters.Add(Item.TargetValue);
				parameters.Add(Item.Cost);
				parameters.Add(Item.BeginTime);
				parameters.Add(Item.EndTime);
			}
		}
		#endregion // IN / OUT
	}

	// 캠페인 갱신
	public class DBQuery_Campaign_Update : DBQuery<DBQuery_Campaign_Update.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_campaign_update";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public CampaignData Item = new CampaignData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.CampaignDBKey);
				parameters.Add(Item.Name);
				parameters.Add(Item.CampaignType);
				parameters.Add(Item.TargetValue);
				parameters.Add(Item.Cost);
				parameters.Add(Item.BeginTime);
				parameters.Add(Item.EndTime);
			}
		}
		#endregion // IN / OUT
	}

	// 캠페인 삭제
	public class DBQuery_Campaign_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Service;
		public override string ProcedureName => "sp_campaign_delete";
	}

	// 캠페인 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_Campaign_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_Campaign_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Service;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public CampaignDataContainer Items { get; private set; } = new CampaignDataContainer();

			public CampaignData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new CampaignData();
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

	// 캠페인 조회( CampaignDBKey )
	public class DBQuery_Campaign_Select : DBQuery_Campaign_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_campaign_select";
	}

	// 캠페인 조회( UserDBKey )
	public class DBQuery_Campaign_Select_By_UserDBKey : DBQuery_Campaign_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_campaign_select_by_userdbkey";
	}
}
