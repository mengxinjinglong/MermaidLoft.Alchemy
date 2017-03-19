using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Microsoft.AspNetCore.Authorization;
using MermaidLoft.Alchemy.BaseDomain.ArticleDomain;
using System.Security.Claims;
using MermaidLoft.Alchemy.QuickWeb.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleQueryService _queryService;
        private readonly IArticleService _service;
        public ArticleController(IArticleQueryService queryService,
            IArticleService service)
        {
            _queryService = queryService;
            _service = service;
        }
        #region Views
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewData["userId"] = HttpContext.User.Claims.FirstOrDefault(item=> item.Type == ClaimTypes.PrimarySid).Value;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List(string key,string type="",int pageIndex=1,int pageSize=10)
        {
            try
            {
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                pageSize = pageSize < 1 ? 10 : pageSize;
                return View(new ArticlesViewModel
                {
                    Key = key,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = await _queryService.GetCountAsync(key, type),
                    Articles = await _queryService.FindForPageAsync(key, type, pageIndex, pageSize)
                });
            }
            catch
            {
            }
            return View();
        }
        [Authorize(Roles = UserType.Admin)]
        public async Task<IActionResult> Edit(string id)
        {
            ViewData["userId"] = HttpContext.User.Claims.FirstOrDefault(item=> item.Type == ClaimTypes.PrimarySid).Value;
            ViewData["id"] = id;
            return View();
        }
        [HttpGet]
        [Route("article/view/{id}.html")]
        public async Task<IActionResult> View(string id)
        {
            try
            {
                var article = await _queryService.FindSingleAsync(new { ArticleId = id });
                if (article != null)
                {
                    article.Hits++;
                    await _service.UpdateAsync(article);
                }
                return View(article);
            }
            catch
            {
            }
            return View();
        }
        #endregion
        #region Web Apis
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> Get(string id)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = await _queryService.FindSingleAsync(new { Id = id })
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
        //[Authorize(Roles = UserType.User)]
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> GetPage(string title, string type, int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    TotalCount = await _queryService.GetCountAsync(title, type),
                    Data = await _queryService.FindForPageAsync(title, type, pageIndex, pageSize)
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
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> Post([FromBody]Article item)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                item.Id = Guid.NewGuid().ToString();
                item.AddTime = DateTime.Now;
                var result = await _service.AddAsync(item);
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

        // PUT api/values/5
        [HttpPut]
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> Put([FromBody]Article item)
        {
            try
            {
                var result = await _service.UpdateAsync(item);
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
        [Route("article/delete")]
        [Authorize(Roles = UserType.Admin)]
        public async Task<ResultMessage> Delete(string id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
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
