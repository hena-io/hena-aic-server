using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;
using Hena.Shared;
using Hena.Shared.Data;

namespace Hena.DB
{
	public class COMMON_IN_DATA_EMPTY : DBQueryBase.IN_BASE
	{
		public override void FillParameters(List<object> parameters)
		{
		}
	}

	public class COMMON_IN_DATA_DBKeyOnly : DBQueryBase.IN_BASE
	{
		public long DBKey { get; set; } = GlobalDefine.INVALID_DBKEY;

		public override void FillParameters(List<object> parameters)
		{
			parameters.Add(DBKey);
		}
	}

	public class COMMON_IN_DATA_DBKeyOffsetLimit : DBQueryBase.IN_BASE
	{
		public long DBKey { get; set; } = GlobalDefine.INVALID_DBKEY;
		public int Offset { get; set; } = 0;
		public int Limit { get; set; } = 0;

		public override void FillParameters(List<object> parameters)
		{
			parameters.Add(DBKey);
			parameters.Add(Offset);
			parameters.Add(Limit);
		}
	}

	public class COMMON_IN_DATA_MachineDBKeyOnly : DBQueryBase.IN_BASE
	{
		public short DBKey { get; set; } = GlobalDefine.INVALID_MACHINE_ID;

		public override void FillParameters(List<object> parameters)
		{
			parameters.Add(DBKey);
		}
	}

	public class COMMON_IN_DATA_EMailOnly : DBQueryBase.IN_BASE
	{
		public string EMail { get; set; } = string.Empty;

		public override void FillParameters(List<object> parameters)
		{
			parameters.Add(EMail);
		}
	}

	public class COMMON_IN_DATA_UserNameOnly : DBQueryBase.IN_BASE
	{
		public string Username { get; set; } = string.Empty;

		public override void FillParameters(List<object> parameters)
		{
			parameters.Add(Username);
		}
	}

	public class COMMON_OUT_DATA_CountOnly : DBQueryBase.IOUT
    {
        public int Count = 0;
        public bool FromDataTable(DataTable table)
        {
            try
            {
                return DBUtility.AsValue(table.Rows[0], "Count", out Count, 0);
            }
            catch(Exception ex)
            {
                Count = 0;
                NLog.LogManager.GetCurrentClassLogger().Error(ex);
                return false;
            }
            
        }
    }
}
