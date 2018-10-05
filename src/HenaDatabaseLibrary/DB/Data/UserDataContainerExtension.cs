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
    public static class UserDataContainerExtension
	{
        #region UserDataContainer
        public static async Task<int> FromDBByLikeEMailAsync(this UserDataContainer item, string email, int offset, int limit = 10)
        {
            item.Clear();

            UserBasicDataContainer basicDataContainer = new UserBasicDataContainer();
            if( await basicDataContainer.FromDBByLikeEMailAsync(email, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach( var it in basicDataContainer.Items)
            {
                var UserData = new UserData();
                UserData.BasicData = it;
                item.Add(UserData);
                tasks.Add(UserData.FromDBByUserDBKeyAsync(it.UserDBKey, false));
            }

            await Task.WhenAll(tasks.ToArray());

            return item.Count;
        }

        public static async Task<int> FromDBByCreateTimeAsync(this UserDataContainer item
            , DateTime beginCreateTime, DateTime endCreateTime, bool sortByCreateTimeDesc, int offset = 0, int limit = 10)
        {
            item.Clear();

            UserBasicDataContainer basicDataContainer = new UserBasicDataContainer();
            if (await basicDataContainer.FromDBByCreateTimeAsync(beginCreateTime, endCreateTime, sortByCreateTimeDesc, offset, limit) == 0)
                return 0;

            List<Task<bool>> tasks = new List<Task<bool>>();
            foreach (var it in basicDataContainer.Items)
            {
                var UserData = new UserData();
                UserData.BasicData = it;
                item.Add(UserData);
                tasks.Add(UserData.FromDBByUserDBKeyAsync(it.UserDBKey, false));
            }

            await Task.WhenAll(tasks.ToArray());
            return item.Count;
        }
        #endregion  // UserDataContainer
    }
}
