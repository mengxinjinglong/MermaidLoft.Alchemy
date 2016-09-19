using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Infrastructure.Utilities;

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

        public IActionResult Register()
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
                if (user.PasswordContent == SecurityCodeUtil.Md5(secureCode))
                {
                    return new ResultMessage
                    {
                        Success = true,
                        Status = EnumStatus.Success,
                        Data = user,
                    };
                }
            }
            return new ResultMessage {
                Success = false,
                Status = EnumStatus.Failure,
                Message = "登陆失败，请确认账号或密码是否正确。" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ResultMessage Get(string id)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindUser(new { Id = id })
                };
            }
            catch(Exception exception)
            {
                return new ResultMessage
                {
                    Success = false,
                    Status = EnumStatus.Failure,
                    Message = exception.Message
                };
            }
        }

        // POST api/values
        [HttpPost]
        public ResultMessage Post(User user)
        {
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.AddTime = DateTime.Now;
                user.LastLoginTime = DateTime.Now;
                user.Status = EnumUserStatus.Normal;
                user.UserType = EnumUserType.Normal;
                user.Version = 0;
                var result = _service.Add(user);
                return new ResultMessage
                {
                    Success = result,
                    Status = result?EnumStatus.Success:EnumStatus.Failure,
                };
            }
            catch (Exception exception)
            {
                return new ResultMessage
                {
                    Success = false,
                    Status = EnumStatus.Failure,
                    Message = exception.Message
                };
            }
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
