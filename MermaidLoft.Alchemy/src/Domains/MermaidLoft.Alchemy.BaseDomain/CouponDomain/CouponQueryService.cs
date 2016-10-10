using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public class CouponQueryService
    {
        public Coupon FindCoupon(string couponId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<Coupon>(new { Id = couponId }, ConfigSettings.CouponTable).SingleOrDefault();
            }
        }

        public IEnumerable<Coupon> FindCoupons(string userId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryList<Coupon>(new { UserId = userId }, ConfigSettings.CouponTable);
            }
        }

        public IEnumerable<Coupon> FindCouponsForPage(string title, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return connection.QueryPaged<Coupon>(null, ConfigSettings.CouponTable, "AddTime", pageIndex, pageSize);
            }
        }
    }
}
