using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using Hena;
using Hena.Shared;
using Hena.Shared.Data;
using MySql.Data.MySqlClient;

// tbl_machine
namespace Hena.DB
{
	// 계정 추가
	public class DBQuery_Machine_Select : DBQuery<DBQuery_Machine_Select.IN_DATA, DBQuery_Machine_Select.OUT_DATA>
	{
		public override DBType DBType => DBType.Hena_AIC_Config;
		public override string ProcedureName => "sp_select_machine";

		#region IN / OUT
		public class IN_DATA : IN_BASE
		{
			public string MacAddress = string.Empty;
			public int Port = 0;
			public override void FillParameters(List<object> parameters)
			{
				parameters.Add(MacAddress);
				parameters.Add(Port);
			}
		}

		public class OUT_DATA : IOUT
		{
			public short MachineId = GlobalDefine.INVALID_MACHINE_ID;
			public string MacAddress = string.Empty;
			public int Port = 0;

			public bool FromDataTable(DataTable table)
			{
				try
				{
					if (table.Rows.Count == 0)
						return false;

					var row = table.Rows[0];

					DBUtility.AsValue(row, "MachineId", out MachineId);
					DBUtility.AsValue(row, "MacAddress", out MacAddress);
					DBUtility.AsValue(row, "Port", out Port);
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
}
