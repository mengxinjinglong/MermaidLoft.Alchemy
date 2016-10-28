using Dapper;
using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain.Implementation
{
    public class UserQueryService: IUserQueryService
    {
        public async Task<User> FindUserAsync(object condition)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return (await connection.QueryListAsync<User>(condition, ConfigSettings.UserTable)).SingleOrDefault();
            }
        }

        public async Task<IEnumerable<User>> FindUsersAsync(string account)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryListAsync<User>(new { Account = account }, ConfigSettings.UserTable);
            }
        }

        public async Task<IEnumerable<User>> FindUsersForPageAsync(string userName,int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryPagedAsync<User>(new { UserName = userName }, ConfigSettings.UserTable,"UserName",pageIndex,pageSize);
            }
        }

        public async Task<IEnumerable<User>> FindUsersForPageAsync(int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                var sql = string.Format("SELECT * FROM User limit {0},{1}", (pageIndex - 1) * pageSize, pageSize);
                return await connection.QueryAsync<User>(sql);
            }
        }
    }
}
