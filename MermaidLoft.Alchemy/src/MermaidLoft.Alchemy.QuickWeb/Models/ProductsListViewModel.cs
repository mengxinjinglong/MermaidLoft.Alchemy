using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Models
{
    public class ProductsListViewModel
    {
        public string ProductName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
