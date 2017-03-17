namespace MermaidLoft.Alchemy.Common
{
    public class ConfigSettings
    {
        public static string BaseResourceTable { get; set; }
        public static string UserTable { get; set; }
        public static string ProductTable { get; set; }
        public static string CouponTable { get; set; }
        public static string ArticleTable { get; set; }
        public static void Initialize()
        {
            UserTable = "User";
            ProductTable = "Product";
            CouponTable = "Coupon";
            BaseResourceTable = "BaseResource";
            ArticleTable = "Article";
        }
    }
}
