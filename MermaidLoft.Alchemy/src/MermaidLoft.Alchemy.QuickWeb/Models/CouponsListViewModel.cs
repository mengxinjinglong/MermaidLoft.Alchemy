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
        public IEnumerable<Coupon> Coupons { get; set; }

        public string Pagination {
            get {
                return new CreatePaginationHandle()
                    .Create(PageIndex,PageSize, 
                    (TotalCount + 1) / PageSize,
                    3,
                    "&title="+Title);
            }
        }
    }
}
