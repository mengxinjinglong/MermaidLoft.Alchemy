using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public class ProductQueryService
    {
        public async Task<Product> FindProductAsync(string productId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return (await connection.QueryListAsync<Product>(new { Id = productId }, ConfigSettings.ProductTable)).SingleOrDefault();
            }
        }

        public async Task<IEnumerable<Product>> FindProductsAsync(string userId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryListAsync<Product>(new { UserId = userId }, ConfigSettings.ProductTable);
            }
        }

        public async Task<IEnumerable<Product>> FindProductsForPageAsync(string productName, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryPagedAsync<Product>(null, ConfigSettings.ProductTable, "ProductName", pageIndex, pageSize);
            }
        }
    }
}
