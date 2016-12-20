using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain.Implementation
{
    public class CouponService: ICouponService
    {
        public async Task<bool> AddAsync(IList<Coupon> coupons)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in coupons)
                        {
                            await connection.InsertAsync(item, ConfigSettings.CouponTable, transaction);
                        }
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        throw exception;
                    }
                }
            }
            return true;
        }

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
