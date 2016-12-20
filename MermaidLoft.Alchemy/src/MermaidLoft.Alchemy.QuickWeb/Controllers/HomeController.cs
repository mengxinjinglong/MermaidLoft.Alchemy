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
        public async Task<IActionResult> Index(string title, int pageIndex=1, int pageSize=20)
        {
            return View(new CouponsListViewModel {
                Title = title,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = await _couponqueryService.SearchCouponsForPageCountAsync(title),
                Coupons = await _couponqueryService.SearchCouponsForPageAsync(title, pageIndex, pageSize)
            });
        }

        [HttpGet]
        public async Task<IActionResult> Products(string productName, int pageIndex = 1, int pageSize = 20)
        {
            return View(new ProductsListViewModel
            {
                ProductName = productName,
                PageIndex = pageIndex+1,
                PageSize = pageSize,
                TotalCount = await _productQueryService.SearchProductsForPageCountAsync(productName),
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
