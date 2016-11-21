using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Models
{
    public class CouponsListViewModel
    {
        public string ShopName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }
    }
}
