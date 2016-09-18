using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class UserController : Controller
    {
        UserQueryService _queryService;
        UserService _service;
        public UserController()
        {
            _queryService = new UserQueryService();
            _service = new UserService();

        }
        #region View
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region WebApi
        // GET: api/values
        [HttpPost]
        public ResultMessage LoginOn(string userName,string secureCode)
        {
            var user = _queryService.FindUser(new {UserName=userName });
            if (user != null)
            {
                
            }
            return new ResultMessage {
                Status = EnumStatus.Failure,
                Message = "登陆失败，请确认账号和密码是否正确。" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(string id)
        {
            return  _queryService.FindUser(new { Id = id });
            
        }

        // POST api/values
        [HttpPost]
        public void Post(User user)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(User user)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
