using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain.Implementation
{
    public class ProductService:IProductService
    {
        public async Task<bool> AddAsync(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
               return await connection.InsertAsync(product, ConfigSettings.ProductTable) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.UpdateAsync(product, new { Id = product.Id }, ConfigSettings.ProductTable) > 0;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.DeleteAsync(new { Id = id }, ConfigSettings.ProductTable) > 0;
            }
        }
    }
}
