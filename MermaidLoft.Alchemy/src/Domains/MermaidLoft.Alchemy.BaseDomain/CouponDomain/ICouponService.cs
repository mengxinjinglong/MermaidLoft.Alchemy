using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public interface ICouponService
    {
        Task<bool> AddAsync(IList<Coupon> coupons);
        Task<bool> AddAsync(Coupon coupon);
        Task<bool> UpdateAsync(Coupon coupon);
        Task<bool> DeleteAsync(string id);
    }
}
