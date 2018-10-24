using Hena.DB;
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
    public static class MiningReportDataExtension
	{
		public static bool FromDBTable(this MiningReportData item, DataRow row)
		{
			if (row == null)
				return false;

			row.Copy(item);
			return true;
		}
	}
}
