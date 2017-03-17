using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ArticleDomain
{
    public interface IArticleQueryService
    {
        Task<Article> FindSingleAsync(object condition);
        Task<IEnumerable<Article>> FindForPageAsync(string title, string type, int pageIndex, int pageSize);
        Task<int> GetCountAsync(string title, string type);
    }
}
