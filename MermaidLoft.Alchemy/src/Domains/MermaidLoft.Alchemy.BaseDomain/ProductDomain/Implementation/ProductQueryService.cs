using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain.Implementation
{
    public class ProductQueryService:IProductQueryService
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

        public async Task<IEnumerable<Product>> FindProductsForPageAsync(string userId, string productName, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                object condition ;
                if (string.IsNullOrEmpty(productName))
                {
                    condition = new { UserId = userId };
                }
                else
                {
                    condition = new { UserId = userId, ProductName = productName };
                }
                return await connection.QueryPagedAsync<Product>(condition, ConfigSettings.ProductTable, "ProductName", pageIndex, pageSize);
            }
        }
    }
}
