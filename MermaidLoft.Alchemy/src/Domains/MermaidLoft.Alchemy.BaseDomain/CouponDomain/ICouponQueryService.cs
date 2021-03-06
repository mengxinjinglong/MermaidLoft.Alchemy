﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public interface ICouponQueryService
    {
        Task<Coupon> FindCouponAsync(string couponId);
        Task<IEnumerable<Coupon>> FindCouponsAsync(string userId);
        Task<IEnumerable<Coupon>> FindCouponsForPageAsync(string userId, string title, int pageIndex, int pageSize);
        IEnumerable<string> FindAllProduceType();
        Task<IEnumerable<Coupon>> SearchCouponsForPageAsync(string title,string productTypeName, int pageIndex, int pageSize);
        Task<int> SearchCouponsForPageCountAsync(string title,string productTypeName);
    }
}
