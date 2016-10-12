using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public class ProductQueryService
    {
        public Product FindProduct(string productId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<Product>(new { Id = productId }, ConfigSettings.ProductTable).SingleOrDefault();
            }
        }

        public IEnumerable<Product> FindProducts(string userId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<Product>(new { UserId = userId }, ConfigSettings.ProductTable);
            }
        }

        public IEnumerable<Product> FindProductsForPage(string productName, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryPaged<Product>(null, ConfigSettings.ProductTable, "ProductName", pageIndex, pageSize);
            }
        }
    }
}
