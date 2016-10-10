using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public class CouponService
    {
        public bool Add(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.Insert(coupon, ConfigSettings.CouponTable) > 0;
            }
        }

        public bool Update(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
               return connection.Update(coupon, new { Id = coupon.Id }, ConfigSettings.CouponTable) > 0;
            }
        }

        public bool Delete(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.Delete(new { Id = id }, ConfigSettings.CouponTable) > 0;
            }
        }
    }
}
