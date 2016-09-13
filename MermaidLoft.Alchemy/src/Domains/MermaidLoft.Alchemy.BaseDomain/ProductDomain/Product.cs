using System;

namespace MermaidLoft.Alchemy.BaseDomain.ProductDomain
{
    public class Product
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Url { get; set; }
        public double Price { get; set; }
        public double DiscountPrice { get; set; }
        public string PictureUrl { get; set; }
        public string UserId { get; set; }
        public DateTime AddTime { get; set; }
        public int Version { get; set; }
    }
}
