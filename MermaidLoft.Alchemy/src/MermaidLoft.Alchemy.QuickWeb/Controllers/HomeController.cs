using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICouponQueryService _couponqueryService;
        private readonly IProductQueryService _productQueryService;

        public HomeController(ICouponQueryService couponqueryService,
            IProductQueryService productQueryService)
        {
            _couponqueryService = couponqueryService;
            _productQueryService = productQueryService;
        }

        #region View
        public IActionResult Index()
        {
            return View();

        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            HttpContext.Authentication.SignOutAsync("UserToken");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }
        #endregion

        #region WEB API
        [HttpGet]
        public async Task<ResultMessage> SearchCoupons(string shopName, int pageIndex, int pageSize)
        {
            return new ResultMessage {
                Success = true,
                Status = EnumStatus.Success,
                Data = _couponqueryService.SearchCouponsForPageAsync(shopName,pageIndex,pageSize)
            };
        }

        [HttpGet]
        public async Task<ResultMessage> SearchProducts(string productName, int pageIndex, int pageSize)
        {
            return new ResultMessage
            {
                Success = true,
                Status = EnumStatus.Success,
                Data = _couponqueryService.SearchCouponsForPageAsync(productName, pageIndex, pageSize)
            };
        }
        #endregion

    }
}
