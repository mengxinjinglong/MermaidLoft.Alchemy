
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.BaseResourceDomain
{
    public interface IBaseResourceService
    {
        Task<bool> AddAsync(BaseResource resource);
        Task<bool> UpdateAsync(BaseResource resource);
        Task<bool> DeleteAsync(string id);
    }
}
