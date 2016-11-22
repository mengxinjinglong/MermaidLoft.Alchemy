using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Models
{
    public class ProductsListViewModel
    {
        public string ProductName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Pagination
        {
            get
            {
                return new CreatePaginationHandle()
                    .Create(PageIndex, PageSize, 
                    (TotalCount + 1) / PageSize, 
                    3,//显示两侧的N个节点
                    "&productName=" + ProductName);
            }
        }
    }
}
