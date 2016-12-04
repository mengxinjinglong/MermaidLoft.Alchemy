using System;

namespace MermaidLoft.Alchemy.BaseDomain.CouponDomain
{
    public class Coupon
    {
        public string Id { get; set; }
        public string BaseUrl { get; set; }
        public string UserId { get; set; }
        public string Title{get;set; }
        public string ShopName { get;set; }
        public string Url{get;set; }
        public string ProductUrl { get; set; }
        public string ProductName { get; set; } //add 2016-12-03
        public string PictureUrl { get; set; }
        public string PictureType { get; set; } //add 2016-12-03
        public string ProductDescription { get; set; }
        public double Price { get; set; } //add 2016-12-03
        public int RestCount { get; set; }
        public string Description{get;set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AddTime { get; set; }
        public bool IsExpired{ get;set;}
        public int Version { get; set; }
    }
}
