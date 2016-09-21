using Infrastructure.Dapper;
using MermaidLoft.Alchemy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public class CouponService
    {
        public void Add(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Insert(coupon, ConfigSettings.CouponTable);
            }
        }

        public void Update(Coupon coupon)
        {
            using (var connection = ConnectionConfig.Instance.GetConnection())
            {
                connection.Update(coupon, new { Id = coupon.Id }, ConfigSettings.CouponTable);
            }
        }
    }
}
