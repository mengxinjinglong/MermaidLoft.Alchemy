﻿using System;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public class Coupon
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title{get;set; }
        public string ShopName { get;set; }
        public string Url{get;set; }
        public int RestCount { get; set; }
        public string Description{get;set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsExpired{ get;set;}
    }
}