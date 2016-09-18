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

        public IEnumerable<UserDto> FindUsers(string userName)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<UserDto>(new { UserName = userName }, ConfigSettings.UserTable);
            }
        }

        public IEnumerable<User> FindUsersForPage(string userName,int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryPaged<User>(new { UserName = userName }, ConfigSettings.UserTable,"UserName",pageIndex,pageSize);
            }
        }
    }
}
