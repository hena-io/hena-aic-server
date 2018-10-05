using Hena.DB;
using Hena.Shared.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
    public static class CampaignDataExtension
	{
		public static bool FromDBTable(this CampaignData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserDBKey", out item.UserDBKey, CampaignData.Default.UserDBKey);
			DBUtility.AsValue(row, "CampaignDBKey", out item.CampaignDBKey, CampaignData.Default.CampaignDBKey);
			DBUtility.AsValue(row, "Name", out item.Name, CampaignData.Default.Name);
			DBUtility.AsValue(row, "CampaignType", out item.CampaignType, CampaignData.Default.CampaignType);
			DBUtility.AsValue(row, "TargetValue", out item.TargetValue, CampaignData.Default.TargetValue);
			DBUtility.AsValue(row, "Cost", out item.Cost, CampaignData.Default.Cost);
			DBUtility.AsValue(row, "BeginTime", out item.BeginTime, CampaignData.Default.BeginTime);
			DBUtility.AsValue(row, "EndTime", out item.EndTime, CampaignData.Default.EndTime);
			DBUtility.AsValue(row, "IsPause", out item.IsPause, CampaignData.Default.IsPause);
			DBUtility.AsValue(row, "IsDeleted", out item.IsDeleted, CampaignData.Default.IsDeleted);
			DBUtility.AsValue(row, "DeletedTime", out item.DeletedTime, CampaignData.Default.DeletedTime);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime, CampaignData.Default.CreateTime);
			DBUtility.AsValue(row, "LastUpdate", out item.LastUpdate, CampaignData.Default.LastUpdate);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this CampaignData item, DBKey campaignDBKey)
        {
            var query = new DBQuery_Campaign_Select();
            query.IN.DBKey = campaignDBKey;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref item);
            return true;
        }
	}
}
