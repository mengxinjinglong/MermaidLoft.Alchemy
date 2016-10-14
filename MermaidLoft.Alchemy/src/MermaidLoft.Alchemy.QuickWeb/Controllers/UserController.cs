using System;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class UserController : Controller
    {
        UserQueryService _queryService;
        UserService _service;
        public UserController()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            _queryService = new UserQueryService();
            _service = new UserService();

        }
        #region View
        // GET: /<controller>/
        //[Authorize(Roles = "Users")]
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

        #region Api
        [HttpPost]
        public ResultMessage LoginOn([FromBody]User u)
        {
            var user = _queryService.FindUser(new {Account= u.Account });
            if (user != null)
            {
                if (user.SecureCode == SecurityCodeUtil.Md5(u   .SecureCode))
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

        [HttpGet]
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
        [HttpGet]
        public ResultMessage GetPage(string userName,string account,int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindUsersForPage(userName,pageIndex,pageSize)
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

        [HttpGet]
        public ResultMessage GetPage1(int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindUsersForPage(pageIndex, pageSize)
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

        [HttpPost]
        public ResultMessage Post([FromBody]User user)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.SecureCode = SecurityCodeUtil.Md5(user.SecureCode);
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
        [HttpPut]
        public ResultMessage Put([FromBody]User user)
        {
            try
            {
                var result = _service.Update(user);
                return new ResultMessage
                {
                    Success = result,
                    Status = result ? EnumStatus.Success : EnumStatus.Failure,
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

        // DELETE api/values/5
        [HttpDelete]
        [Route("user/delete")]
        public ResultMessage Delete(string id)
        {
            try
            {
                var result = _service.Delete(id);
                return new ResultMessage
                {
                    Success = result,
                    Status = result ? EnumStatus.Success : EnumStatus.Failure,
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
        #endregion
    }

    public class UserIdentity : Microsoft.AspNetCore.Identity.IUserLoginStore
    {

    }
}
