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
    public static class AdDesignDataExtension
	{
		public static bool FromDBTable(this AdDesignData item, DataRow row)
		{
			if (row == null)
				return false;

			DBUtility.AsValue(row, "UserId", out item.UserId, AdDesignData.Default.UserId);
			DBUtility.AsValue(row, "CampaignId", out item.CampaignId, AdDesignData.Default.CampaignId);
			DBUtility.AsValue(row, "AdDesignId", out item.AdDesignId, AdDesignData.Default.AdDesignId);
			DBUtility.AsValue(row, "Name", out item.Name, AdDesignData.Default.Name);
			DBUtility.AsValue(row, "AdDesignType", out item.AdDesignType, AdDesignData.Default.AdDesignType);
			DBUtility.AsValue(row, "ResourceName", out item.ResourceName, AdDesignData.Default.ResourceName);
			DBUtility.AsValue(row, "DestinationUrl", out item.DestinationUrl, AdDesignData.Default.DestinationUrl);
			DBUtility.AsValue(row, "IsPause", out item.IsPause, AdDesignData.Default.IsPause);
			DBUtility.AsValue(row, "IsDeleted", out item.IsDeleted, AdDesignData.Default.IsDeleted);
			DBUtility.AsValue(row, "DeletedTime", out item.DeletedTime, AdDesignData.Default.DeletedTime);
			DBUtility.AsValue(row, "CreateTime", out item.CreateTime, AdDesignData.Default.CreateTime);
			DBUtility.AsValue(row, "LastUpdate", out item.LastUpdate, AdDesignData.Default.LastUpdate);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AdDesignData item, DBKey adDesignId)
        {
            var query = new DBQuery_AdDesign_Select();
            query.IN.DBKey = adDesignId;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.CopyTo(ref item);
            return true;
        }
	}
}
