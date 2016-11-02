using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public interface IUserQueryService
    {
        Task<User> FindUserAsync(object condition);
        Task<IEnumerable<User>> FindUsersAsync(string account);
        Task<IEnumerable<User>> FindUsersForPageAsync(string userName, int pageIndex, int pageSize);
        Task<int> GetCountAsync(string userName);
        Task<IEnumerable<User>> FindUsersForPageAsync(int pageIndex, int pageSize);
    }
}
