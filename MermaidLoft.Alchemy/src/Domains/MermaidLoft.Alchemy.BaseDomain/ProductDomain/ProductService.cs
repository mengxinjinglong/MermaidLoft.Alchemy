using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public class ProductService
    {
        public void Add(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Insert(product, ConfigSettings.ProductTable);
            }
        }

        public void Update(Product product)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Update(product, new { Id = product.Id }, ConfigSettings.ProductTable);
            }
        }
    }
}
