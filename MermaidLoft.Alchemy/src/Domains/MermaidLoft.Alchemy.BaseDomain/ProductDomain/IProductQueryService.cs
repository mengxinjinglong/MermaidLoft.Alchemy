using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public interface IProductQueryService
    {
        Task<Product> FindProductAsync(string productId);
        Task<IEnumerable<Product>> FindProductsAsync(string userId);
        Task<IEnumerable<Product>> FindProductsForPageAsync(string userId,string productName, int pageIndex, int pageSize);
        Task<IEnumerable<Product>> SearchProductsForPageAsync(string productName, int pageIndex, int pageSize);
    }
}
