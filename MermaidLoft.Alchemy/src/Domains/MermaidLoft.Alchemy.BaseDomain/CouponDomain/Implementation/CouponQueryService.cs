using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain.Implementation
{
    public class CouponQueryService:ICouponQueryService
    {
        public async Task<Coupon> FindCouponAsync(string couponId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return (await connection.QueryListAsync<Coupon>(new { Id = couponId }, ConfigSettings.CouponTable)).SingleOrDefault();
            }
        }

        public async Task<IEnumerable<Coupon>> FindCouponsAsync(string userId)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryListAsync<Coupon>(new { UserId = userId }, ConfigSettings.CouponTable);
            }
        }

        public async Task<IEnumerable<Coupon>> FindCouponsForPageAsync(string title, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.QueryPagedAsync<Coupon>(null, ConfigSettings.CouponTable, "AddTime", pageIndex, pageSize);
            }
        }
    }
}
