using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Dapper;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using MermaidLoft.Alchemy.BaseDomain.ProductDomain;

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class HomeController : Controller
    {
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

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public object Mysql()
        {
            try
            {
                //new UserService().Add(new User { Id=Guid.NewGuid().ToString(),UserName="美人鱼",PasswordContent="123qwe",Account= "Mermaid", Version=0});
                //new ProductService().Add(new Product {
                //    Id = Guid.NewGuid().ToString(),
                //    ProductName = "埃伦2016秋装新款印花蕾丝连衣裙欧美时尚圆领修身开叉系带连衣裙",
                //    Url = "http://item.taobao.com/item.htm?id=526404708154",
                //    PictureUrl = "http://img03.taobaocdn.com/bao/uploaded/i3/TB1G3WQMVXXXXcZXXXXXXXXXXXX_!!0-item_pic.jpg",
                //    AddTime = DateTime.Now,
                //    DiscountPrice = 0,
                //    Price = 0,
                //    UserId= "0abf8ccf-3bde-4dac-b31f-5a43895f002e",
                //    Version=0,
                //});
                return new UserQueryService().FindUsersForPage("%",1,10);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }
    }
}
