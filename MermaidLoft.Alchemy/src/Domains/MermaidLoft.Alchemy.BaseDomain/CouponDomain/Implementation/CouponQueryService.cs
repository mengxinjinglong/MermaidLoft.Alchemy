using Dapper;
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

        public async Task<IEnumerable<Coupon>> SearchCouponsForPageAsync(string shopName, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                string conditionSql = string.Empty;
                object condition = null;
                if (!string.IsNullOrEmpty(shopName))
                {
                    condition = new { ShopName = string.Format("%{0}%", shopName) };
                    conditionSql = " where ShopName like @ShopName ";
                }
                var sql = string.Format("SELECT * FROM {0} {1} limit {2},{3}",
                        ConfigSettings.CouponTable, conditionSql,
                        (pageIndex - 1) * pageSize, pageSize);
                return await connection.QueryAsync<Coupon>(sql, condition);
            }
        }

        public async Task<int> SearchCouponsForPageCountAsync(string shopName)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                string conditionSql = string.Empty;
                object condition = null;
                if (!string.IsNullOrEmpty(shopName))
                {
                    condition = new { ShopName = string.Format("%{0}%", shopName) };
                    conditionSql = " where ShopName like @ShopName ";
                }
                var sql = string.Format("SELECT count(*) FROM {0} {1}",
                        ConfigSettings.CouponTable, conditionSql);
                return (await connection.QueryAsync<int>(sql, condition)).Single();
            }
        }
    }
}
