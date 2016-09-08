using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Data;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserService
    {
        public UserService()
        {
        }

        public void Add(User user)
        {
            using (var connection =ConnectionConfig.Instance.GetConnection())
            {
                connection.Insert(user,ConfigSettings.UserTable);
            }
        }

        public void Update(User user)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Update(user, new { Id=user.Id },ConfigSettings.UserTable);
            }
        }
    }
}
