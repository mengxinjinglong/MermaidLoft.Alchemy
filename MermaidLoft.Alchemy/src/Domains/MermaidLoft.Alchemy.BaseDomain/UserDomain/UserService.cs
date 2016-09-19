using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserService
    {
        public UserService()
        {
        }

        public bool Add(User user)
        {
            using (var connection =ConnectionConfig.Instance.GetConnection())
            {
                return connection.Insert(user,ConfigSettings.UserTable)>0;
            }
        }

        public bool Update(User user)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.Update(user, new { Id=user.Id },ConfigSettings.UserTable)>0;
            }
        }
    }
}
