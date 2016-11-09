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

        public async Task<IEnumerable<Coupon>> FindCouponsForPageAsync(string userId, string title, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                object condition;
                if (string.IsNullOrEmpty(title))
                {
                    condition = new { UserId = userId };
                }
                else
                {
                    condition = new { UserId = userId, Title = title };
                }
                return await connection.QueryPagedAsync<Coupon>(condition, ConfigSettings.CouponTable, "AddTime", pageIndex, pageSize);
            }
        }
    }
}
