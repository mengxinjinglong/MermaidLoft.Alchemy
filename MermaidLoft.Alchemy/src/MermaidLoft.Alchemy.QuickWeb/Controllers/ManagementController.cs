using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MermaidLoft.Alchemy.QuickWeb.Core;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IAuthorizeValidator _authorizeValidator;
        public ManagementController(
            IAuthorizeValidator authorizeValidator)
        {
            _authorizeValidator = authorizeValidator;
        }
        // GET: /<controller>/
        [Authorize(Roles = UserType.User)]
        public async Task<IActionResult> Index()
        {
            if (await _authorizeValidator.TryValidateAdminAsync(HttpContext.User))
            {
                ViewData["IsAdmin"] = true;
            }
            return View();
        }
    }
}
