using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using MermaidLoft.Alchemy.QuickWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICouponQueryService _couponqueryService;
        private readonly IProductQueryService _productQueryService;
        private static IEnumerable<string> ProductTypeNames;
        private readonly static object _lockObject = new object();
        public HomeController(ICouponQueryService couponqueryService,
            IProductQueryService productQueryService)
        {
            try
            {
                _couponqueryService = couponqueryService;
                _productQueryService = productQueryService;
                if (ProductTypeNames == null|| !ProductTypeNames.Any())
                {
                    lock (_lockObject)
                    {
                        if (ProductTypeNames == null)
                        {
                            ProductTypeNames = _couponqueryService.FindAllProduceType();
                        }
                    }
                }
            }
            catch
            {

            }
        }
        #region View
        [HttpGet]
        public async Task<IActionResult> Index(string title,string productTypeName, int pageIndex=1, int pageSize=20)
        {
            return View(new CouponsListViewModel {
                Title = title,
                PageIndex = pageIndex,
                PageSize = pageSize,
                ProductTypeName = productTypeName,
                ProductTypeNames = ProductTypeNames,
                TotalCount = await _couponqueryService.SearchCouponsForPageCountAsync(title,productTypeName),
                Coupons = await _couponqueryService.SearchCouponsForPageAsync(title, productTypeName, pageIndex, pageSize)
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
        [HttpGet]
        [Route("root.txt")]
        public IActionResult Robots()
        {
            return View();
        }
        #endregion

        #region WEB API
        /*
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
        }*/
        #endregion

    }
}
