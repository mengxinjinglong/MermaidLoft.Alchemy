using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public interface ICouponQueryService
    {
        Task<Coupon> FindCouponAsync(string couponId);
        Task<IEnumerable<Coupon>> FindCouponsAsync(string userId);
        Task<IEnumerable<Coupon>> FindCouponsForPageAsync(string title, int pageIndex, int pageSize);
    }
}
