using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.BaseResourceDomain
{
    public interface IBaseResourceQueryService
    {
        Task<BaseResource> FindSingleAsync(object condition);
        Task<IEnumerable<BaseResource>> FindForPageAsync(string name, string type, int pageIndex, int pageSize);
        Task<int> GetCountAsync(string name,string type);
    }
}
