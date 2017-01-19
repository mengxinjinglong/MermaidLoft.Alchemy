using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Models
{
    public class CouponsListViewModel
    {
        public string Title { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string ProductTypeName{ get; set; }
        public IEnumerable<string> ProductTypeNames { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }

        public string Pagination {
            get {
                return new CreatePaginationHandle()
                    .Create(PageIndex,PageSize, 
                    (TotalCount + 1) / PageSize,
                    3,
                    string.Format("&title={0}&productTypeName={1}", Title,ProductTypeName));
            }
        }
    }
}
