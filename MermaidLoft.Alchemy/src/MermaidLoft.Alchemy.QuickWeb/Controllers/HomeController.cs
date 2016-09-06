using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Dapper;

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
                ConnectionConfig.Instance.SetConnectString("server=127.0.0.1;database=Alchemy;uid=root;pwd=123456;");
                using (IDbConnection connection = ConnectionConfig.Instance.GetConnection())
                {
                    connection.Open();
                    //connection.Insert(new User {Id=Guid.NewGuid().ToString(),Account = "Mercurial",PasswordContent = "123567",UserName = "墨丘利",Version = 0}, "User");
                    return connection.QueryList<User>(null,"User");
                }
            }
            catch (Exception exception)
            {
                return exception;
            }
        }
    }


    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public string PasswordContent { get; set; }
        public int Version { get; set; }
    }
}
