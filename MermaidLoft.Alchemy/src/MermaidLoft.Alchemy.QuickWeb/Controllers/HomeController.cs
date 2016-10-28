using Microsoft.AspNetCore.Mvc;

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
            HttpContext.Authentication.SignOutAsync("UserToken");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}
