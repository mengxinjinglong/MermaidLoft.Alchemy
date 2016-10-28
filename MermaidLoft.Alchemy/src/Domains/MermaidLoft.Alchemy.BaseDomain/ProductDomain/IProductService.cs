using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public interface IProductService
    {
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
