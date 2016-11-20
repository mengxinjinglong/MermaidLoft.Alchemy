using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using MermaidLoft.Alchemy.QuickWeb.Models;
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
        [HttpGet]
        public async Task<IActionResult> Index(string shopName, int pageIndex=1, int pageSize=40)
        {
            return View(new CouponsListViewModel {
                ShopName = shopName,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Coupons = await _couponqueryService.SearchCouponsForPageAsync(shopName, pageIndex, pageSize)
            });
        }

        [HttpGet]
        public async Task<IActionResult> Products(string productName, int pageIndex = 1, int pageSize = 40)
        {
            return View(new ProductsListViewModel
            {
                ProductName = productName,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Products = await _productQueryService.SearchProductsForPageAsync(productName, pageIndex, pageSize)
            });
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
