using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain.Implementation
{
    public class CouponService: ICouponService
    {
        public async Task<bool> AddAsync(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.InsertAsync(coupon, ConfigSettings.CouponTable) > 0;
            }
        }

        public async Task<bool> UpdateAsync(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
               return await connection.UpdateAsync(coupon, new { Id = coupon.Id }, ConfigSettings.CouponTable) > 0;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                return await connection.DeleteAsync(new { Id = id }, ConfigSettings.CouponTable) > 0;
            }
        }
    }
}
