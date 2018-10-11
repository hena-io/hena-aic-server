using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Hena.Shared.Data
{
	public class AdResourceDataContainer
		: ListDataContainer<AdResourceDataContainer, AdResourceData>
	{
		public AdResourceData FindByUserId(DBKey userId)
		{
			return Find(item => item.UserId == userId);
		}

		public AdResourceData FindByAdResourceId(DBKey adResourceId)
		{
			return Find(item => item.AdResourceId == adResourceId);
		}
	}
}
