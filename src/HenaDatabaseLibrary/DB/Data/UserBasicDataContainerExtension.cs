using Hena.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena.Shared.Data
{
    public static class UserBasicDataContainerExtension
	{
        public static async Task<int> FromDBByEMailsAsync(this UserBasicDataContainer item, string[] emails)
        {
            item.Clear();
            if (emails.Length == 0)
                return 0;

            List<DBQuery_User_Select_By_EMail> queries = new List<DBQuery_User_Select_By_EMail>();
            foreach (var it_email in emails)
            {
                var query = new DBQuery_User_Select_By_EMail();
                query.IN.EMail = it_email;
                queries.Add(query);
            }

            await DBThread.Instance.ReqQueryAsync(queries.ToArray());

            foreach (var it in queries)
            {
                if (it.OUT.FirstItem == null)
                    continue;
                
                item.Add(it.OUT.FirstItem);
            }

            return item.Count;
        }

        public static async Task<int> FromDBByLikeEMailAsync(this UserBasicDataContainer item, string email, int offset = 0, int limit = 10)
        {
            item.Clear();
            var query = new DBQuery_User_Select_By_LikeEMail();
            query.IN.EMail = email;
            query.IN.Offset = offset;
            query.IN.Limit = limit;
            var result = await DBThread.Instance.ReqQueryAsync(query);
            query.OUT.Items.CopyTo(ref item);
            return item.Count;
        }

        public static async Task<int> FromDBByLikeEMailCountAsync(this UserBasicDataContainer item, string email)
        {
            var query = new DBQuery_User_Select_By_LikeEMail_Count();
            query.IN.EMail = email;
            await DBThread.Instance.ReqQueryAsync(query);
            return query.OUT.Count;
        }

        public static async Task<int> FromDBCountAsync(this UserBasicDataContainer item)
        {
            var query = new DBQuery_User_Select_Count();
            await DBThread.Instance.ReqQueryAsync(query);
            return query.OUT.Count;
        }

        public static async Task<int> FromDBByCreateTimeAsync(this UserBasicDataContainer item
            , DateTime beginCreateTime, DateTime endCreateTime, bool sortByCreateTimeDesc, int offset = 0, int limit = 10)
        {
            item.Clear();
            var query = new DBQuery_User_Select_By_CreateTime();
            query.IN.BeginCreateTime = beginCreateTime;
            query.IN.EndCreateTime = endCreateTime;
            query.IN.SortByCreateTimeDesc = sortByCreateTimeDesc;
            query.IN.Offset = offset;
            query.IN.Limit = limit;
            var result = await DBThread.Instance.ReqQueryAsync(query);
            query.OUT.Items.CopyTo(ref item);
            return item.Count;
        }
    }
}
