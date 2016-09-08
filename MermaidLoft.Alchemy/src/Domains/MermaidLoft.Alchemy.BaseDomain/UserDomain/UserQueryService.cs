using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserQueryService
    {
        public User FindUser(string userId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<User>(new { Id=userId}, ConfigSettings.UserTable).SingleOrDefault();
            }
        }

        public IEnumerable<UserDto> FindUsers(string userName)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<UserDto>(new { UserName = userName }, ConfigSettings.UserTable);
            }
        }
    }
}
