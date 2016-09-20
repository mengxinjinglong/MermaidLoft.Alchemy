using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class ProductController : Controller
    {
        #region VIEW
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(string id)
        {
            return View();
        }
        #endregion

        #region API

        #endregion
    }
}
