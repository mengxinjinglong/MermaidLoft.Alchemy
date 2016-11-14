using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public interface ICouponQueryService
    {
        Task<Coupon> FindCouponAsync(string couponId);
        Task<IEnumerable<Coupon>> FindCouponsAsync(string userId);
        Task<IEnumerable<Coupon>> FindCouponsForPageAsync(string userId, string title, int pageIndex, int pageSize);

        Task<IEnumerable<Coupon>> SearchCouponsForPageAsync(string shopName, int pageIndex, int pageSize);
    }
}
