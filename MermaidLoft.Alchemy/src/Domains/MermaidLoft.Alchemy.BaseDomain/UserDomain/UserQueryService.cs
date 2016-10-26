using Dapper;
using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserQueryService
    {
        public User FindUser(object condition)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<User>(condition, ConfigSettings.UserTable).SingleOrDefault();
            }
        }

        public IEnumerable<User> FindUsers(string account)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<User>(new { Account = account }, ConfigSettings.UserTable);
            }
        }

        public IEnumerable<User> FindUsersForPage(string userName,int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryPaged<User>(new { UserName = userName }, ConfigSettings.UserTable,"UserName",pageIndex,pageSize);
            }
        }

        public IEnumerable<User> FindUsersForPage(int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                var sql = string.Format("SELECT * FROM User limit {0},{1}", (pageIndex - 1) * pageSize, pageSize);
                return connection.Query<User>(sql);
            }
        }
    }
}
