﻿using Hena.DB;
using Hena.Library.Extensions;
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

			row.Copy(item);
			return true;
		}
		
        public static async Task<bool> FromDBAsync(this AdDesignData item, DBKey adDesignId)
        {
            var query = new DBQuery_AdDesign_Select();
            query.IN.DBKey = adDesignId;

            await DBThread.Instance.ReqQueryAsync(query);
            if (query.OUT.FirstItem == null)
                return false;

            query.OUT.FirstItem.Copy(item);
            return true;
        }

		public static async Task<bool> ChoiceFromDBAsync(this AdDesignData item, AdDesignTypes.en adDesignType)
		{
			var query = new DBQuery_AdDesign_Choice();
			query.IN.AdDesignType = adDesignType;

			await DBThread.Instance.ReqQueryAsync(query);
			if (query.OUT.FirstItem == null)
				return false;

			query.OUT.FirstItem.Copy(item);
			return true;
		}
	}
}
