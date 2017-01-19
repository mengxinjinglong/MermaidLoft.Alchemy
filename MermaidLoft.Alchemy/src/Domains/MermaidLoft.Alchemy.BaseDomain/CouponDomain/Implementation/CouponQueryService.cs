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

        public async Task<IEnumerable<Coupon>> SearchCouponsForPageAsync(string title, string productTypeName, int pageIndex, int pageSize)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                string conditionSql = string.Empty;
                object condition = null;

                var i = 0;
                if (!string.IsNullOrEmpty(title))
                {
                    i++;//1
                    conditionSql = " and ProductName like @ProductName ";
                }
                if (!string.IsNullOrEmpty(productTypeName))
                {
                    i += 2;//2,一个，3两个
                    conditionSql += " and ProductType=@ProductType ";
                }
                switch (i)
                {
                    case 1:
                        condition = new { ProductName = string.Format("%{0}%", title) };
                        break;
                    case 2:
                        condition = new { ProductType = productTypeName };
                        break;
                    case 3:
                        condition = new
                        {
                            ProductName = string.Format("%{0}%", title),
                            ProductType = productTypeName
                        };
                        break;
                }

                var sql = string.Format("SELECT * FROM {0} where 1=1 {1} order by AddTime desc limit {2},{3}",
                        ConfigSettings.CouponTable, conditionSql,
                        (pageIndex - 1) * pageSize, pageSize);
                return await connection.QueryAsync<Coupon>(sql, condition);
            }
        }

        public async Task<int> SearchCouponsForPageCountAsync(string title, string productTypeName)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                string conditionSql = string.Empty;
                object condition = null;

                var i = 0;
                if (!string.IsNullOrEmpty(title))
                {
                    i++;//1
                    conditionSql = " and ProductName like @ProductName ";
                }
                if (!string.IsNullOrEmpty(productTypeName))
                {
                    i += 2;//2,一个，3两个
                    conditionSql += " and ProductType = @ProductType ";
                }
                switch (i)
                {
                    case 1:
                        condition = new { ProductName = string.Format("%{0}%", title) };
                        break;
                    case 2:
                        condition = new { ProductType = productTypeName };
                        break;
                    case 3:
                        condition = new
                        {
                            ProductName = string.Format("%{0}%", title),
                            ProductType = productTypeName
                        };
                        break;
                }

                var sql = string.Format("SELECT count(*) FROM {0} where 1=1 {1}",
                        ConfigSettings.CouponTable, conditionSql);
                return (await connection.QueryAsync<int>(sql, condition)).Single();
            }
        }
        public IEnumerable<string> FindAllProduceType()
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                var sql = string.Format("SELECT distinct ProductType FROM {0}",
                        ConfigSettings.CouponTable);
                return connection.Query<string>(sql);
            }
        }
    }
}
