using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;
using System.Threading.Tasks;
using Hena.Library.Extensions;

// tbl_ad_history
namespace Hena.DB
{
	// 광고 기록 추가
	public class DBQuery_AdHistory_Insert : DBQuery<DBQuery_AdHistory_Insert.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Report;
		public override string ProcedureName => "sp_ad_history_insert";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public AdHistoryData Item = new AdHistoryData();

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(Item.AdHistoryId);

				parameters.Add(Item.PublisherId);
				parameters.Add(Item.AppId);
				parameters.Add(Item.AdUnitId);

				parameters.Add(Item.AdvertiserId);
				parameters.Add(Item.CampaignId);
				parameters.Add(Item.AdDesignId);

				parameters.Add(Item.IPAddress);
				parameters.Add(Item.UserAgent);

				parameters.Add(Item.CampaignType);
				parameters.Add(Item.AdDesignType);
				parameters.Add(Item.Cost);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 기록 - 노출정보 갱신
	public class DBQuery_AdHistory_Update_Display : DBQuery<DBQuery_AdHistory_Update_Display.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Report;
		public override string ProcedureName => "sp_ad_history_update_display";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey AdHistoryId { get; set; } = GlobalDefine.INVALID_DBKEY;
			public bool IsDisplayed { get; set; } = false;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(AdHistoryId);
				parameters.Add(IsDisplayed);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 기록 - 클릭정보 갱신
	public class DBQuery_AdHistory_Update_Click : DBQuery<DBQuery_AdHistory_Update_Click.IN_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Report;
		public override string ProcedureName => "sp_ad_history_update_click";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public DBKey AdHistoryId { get; set; } = GlobalDefine.INVALID_DBKEY;
			public bool IsClicked { get; set; } = false;

			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(AdHistoryId);
				parameters.Add(IsClicked);
			}
		}
		#endregion // IN / OUT
	}

	// 광고 기록 삭제
	public class DBQuery_AdHistory_Delete : DBQuery<COMMON_IN_DATA_DBKeyOnly>
	{
		public override DBType DBType => DBType.Hena_AIC_Report;
		public override string ProcedureName => "sp_ad_history_delete";
	}

	#region Select

	// 광고 기록 정보 조회( OUT 선 정의 )
	public abstract class DBQuery_AdHistory_Select_Base<T_IN>
		: DBQuery<T_IN, DBQuery_AdHistory_Select_Base<T_IN>.OUT_DATA>
		where T_IN : DBQueryBase.IIN, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Report;

		#region IN / OUT
		public class OUT_DATA : IOUT
		{
			public AdHistoryDataContainer Items { get; private set; } = new AdHistoryDataContainer();

			public AdHistoryData FirstItem => Items.FirstItem;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					foreach (DataRow row in table.Rows)
					{
						var item = new AdHistoryData();
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

	// 광고 기록 조회( AdHistoryId )
	public class DBQuery_AdHistory_Select : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select";
	}
	// 광고 기록 조회( PublisherId )
	public class DBQuery_AdHistory_Select_By_PublisherId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_publisherid";
	}
	// 광고 기록 조회( AdvertiserId )
	public class DBQuery_AdHistory_Select_By_AdvertiserId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_advertiserid";
	}
	// 광고 기록 조회( AppId )
	public class DBQuery_AdHistory_Select_By_AppId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_appid";
	}
	// 광고 기록 조회( AdUnitId )
	public class DBQuery_AdHistory_Select_By_AdUnitId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_adunitid";
	}
	// 광고 기록 조회( CampaignId )
	public class DBQuery_AdHistory_Select_By_CampaignId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_campaignid";
	}
	// 광고 기록 조회( AdDesignId )
	public class DBQuery_AdHistory_Select_By_AdDesignId : DBQuery_AdHistory_Select_Base<COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_select_by_addesignid";
	}

	#endregion // Count


	#region Count
	// 광고 기록 갯수 조회( OUT 선 정의 )
	public abstract class DBQuery_AdHistory_Count_Base<T_QUERY, T_IN>
		: DBQuery<T_IN, COMMON_OUT_DATA_CountOnly>
		where T_IN : DBQueryBase.IIN, new()
		where T_QUERY : DBQuery_AdHistory_Count_Base<T_QUERY, T_IN>, new()
	{
		public override DBType DBType => DBType.Hena_AIC_Report;

		protected static async Task<int> CountByDBKeyAsync(T_IN inData)
		{
			T_QUERY query = new T_QUERY();
			inData.Copy(query.IN);
			await DBThread.Instance.ReqQueryAsync(query);
			return query.OUT.Count;
		}
	}

	// 광고 기록 갯수 조회( PublisherId )
	public class DBQuery_AdHistory_Count_By_PublisherId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_PublisherId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_publisherid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	// 광고 기록 갯수 조회( AdvertiserId )
	public class DBQuery_AdHistory_Count_By_AdvertiserId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_AdvertiserId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_advertiserid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	// 광고 기록 갯수 조회( AppId )
	public class DBQuery_AdHistory_Count_By_AppId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_AppId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_appid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	// 광고 기록 갯수 조회( AdUnitId )
	public class DBQuery_AdHistory_Count_By_AdUnitId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_AdUnitId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_adunitid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	// 광고 기록 갯수 조회( CampaignId )
	public class DBQuery_AdHistory_Count_By_CampaignId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_CampaignId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_campaignid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	// 광고 기록 갯수 조회( AdDesignId )
	public class DBQuery_AdHistory_Count_By_AdDesignId : DBQuery_AdHistory_Count_Base<DBQuery_AdHistory_Count_By_AdDesignId, COMMON_IN_DATA_DBKeyOnly>
	{
		public override string ProcedureName => "sp_ad_history_count_by_addesignid";

		public static async Task<int> GetCountAsync(DBKey id)
		{
			return await CountByDBKeyAsync(new COMMON_IN_DATA_DBKeyOnly() { DBKey = id });
		}
	}
	#endregion	// Count
}
