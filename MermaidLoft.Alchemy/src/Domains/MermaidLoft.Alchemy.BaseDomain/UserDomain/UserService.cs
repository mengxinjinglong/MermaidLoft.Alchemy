using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public class UserService
    {
        public UserService()
        {
        }

        public async Task<bool> Add(User user)
        {
            using (var connection =ConnectionConfig.Instance.GetConnection())
            {
                return await connection.InsertAsync(user, ConfigSettings.UserTable) > 0;
            }
        }

        public async Task<bool> Update(User user)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.UpdateAsync(user, new { Id = user.Id }, ConfigSettings.UserTable) > 0;
            }
        }

        public async Task<bool> Delete(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.DeleteAsync(new { Id = id }, ConfigSettings.UserTable) > 0;
            }
        }
    }
}
