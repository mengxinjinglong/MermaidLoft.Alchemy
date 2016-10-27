﻿using System;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.UserDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

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
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = UserType.User)]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        #endregion

        #region Api
        [HttpPost]
        public async Task<ResultMessage> LoginOn([FromBody]User u)
        {
            var message = "";
            try
            {
                Ensure.NotNullOrEmpty(u.Account,"用户账号");
                Ensure.NotNullOrEmpty(u.SecureCode,"密码");
                var user = await _queryService.FindUserAsync(new { Account = u.Account });
                if (user != null)
                {
                    if (user.SecureCode == SecurityCodeUtil.Md5(u.SecureCode))
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.PrimarySid, user.Id));
                        if (user.UserType == EnumUserType.Admin)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, UserType.Admin));
                        }
                        claims.Add(new Claim(ClaimTypes.Role, UserType.User));
                        var identity = new ClaimsIdentity(claims, "claimsLogin");

                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        
                        await HttpContext.Authentication.SignInAsync("UserToken", principal,
                         new AuthenticationProperties
                         {
                             ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                             IsPersistent = false,
                             //AllowRefresh = false
                         });
                        return new ResultMessage
                        {
                            Success = true,
                            Status = EnumStatus.Success,
                            Data = user,
                        };
                    }
                }
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return new ResultMessage
            {
                Success = false,
                Status = EnumStatus.Failure,
                Message = string.IsNullOrEmpty(message) ? "登陆失败，请确认账号或密码是否正确。" : message
            };
        }
        [HttpPost]
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> LoginOut()
        {
            await HttpContext.Authentication.SignOutAsync("UserToken");
            return null;
        }

        [HttpGet]
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Get(string id)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindUserAsync(new { Id = id })
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
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> GetPage(string userName,string account,int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindUsersForPageAsync(userName,pageIndex,pageSize)
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
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> GetPage1(int pageIndex, int pageSize)
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
        [AllowAnonymous]
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Post([FromBody]User user)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                user.Id = Guid.NewGuid().ToString();
                user.SecureCode = SecurityCodeUtil.Md5(user.SecureCode);
                user.AddTime = DateTime.Now;
                user.LastLoginTime = DateTime.Now;
                user.Status = EnumUserStatus.Normal;
                user.UserType = EnumUserType.User;
                user.Version = 0;
               
                //验证
                await new UserValidator().ValidatorAsync(_queryService,user);

                var result = await _service.Add(user);
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Put([FromBody]User user)
        {
            try
            {
                if (HttpContext.User.HasClaim(item => item.Type == ClaimTypes.PrimarySid && item.Value != user.Id)
                    && !HttpContext.User.Claims.Any(item=>item.Type==ClaimTypes.Role&&item.Value=="Admin"))
                {
                    throw new Exception("您无权操作非当前用户信息！");
                }
                var result = await _service.Update(user);
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);
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
}
