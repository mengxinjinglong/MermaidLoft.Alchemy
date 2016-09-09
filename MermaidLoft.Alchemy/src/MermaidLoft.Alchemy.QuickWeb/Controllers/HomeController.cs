using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Dapper;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;

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
                return new UserQueryService().FindUsersForPage("美人鱼",1,10);
            }
            catch (Exception exception)
            {
                return exception;
            }
        }
    }
}
