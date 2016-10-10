using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public class ProductService
    {
        public bool Add(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
               return connection.Insert(product, ConfigSettings.ProductTable) > 0;
            }
        }

        public bool Update(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.Update(product, new { Id = product.Id }, ConfigSettings.ProductTable) > 0;
            }
        }
    }
}
