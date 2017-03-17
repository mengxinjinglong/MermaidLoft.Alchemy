using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ArticleDomain
{
    public interface IArticleService
    {
        Task<bool> AddAsync(Article article);
        Task<bool> UpdateAsync(Article article);
        Task<bool> DeleteAsync(string id);
    }
}
