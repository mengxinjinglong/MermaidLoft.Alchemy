
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.UserDomain
{
    public interface IUserService
    {
        Task<bool> AddAsync(User user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(string id);
    }
}
