using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class MiningHistoryDataContainer
		: ListDataContainer<MiningHistoryDataContainer, MiningHistoryData>
	{
		#region Find
		public MiningHistoryData FindByMiningHistoryId(DBKey adHistoryId)
		{
			return Find(item => item.MiningHistoryId == adHistoryId);
		}
		#endregion // Find

		#region FindAll
		public List<MiningHistoryData> FindAllByUserId(DBKey userId)
		{
			return FindAll(item => item.UserId == userId);
		}
		#endregion // FindAll
	}
}
