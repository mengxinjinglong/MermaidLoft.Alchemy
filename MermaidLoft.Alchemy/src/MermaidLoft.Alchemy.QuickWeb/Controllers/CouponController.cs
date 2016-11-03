using System;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Infrastructure.Spider;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponQueryService _queryService;
        private readonly ICouponService _service;
        private readonly IAuthorizeValidator _authorizeValidator;
        public CouponController(ICouponQueryService queryService, 
            ICouponService service,
            IAuthorizeValidator authorizeValidator)
        {
            _queryService = queryService;
            _service = service;
            _authorizeValidator = authorizeValidator;
        }

        #region View
        // GET: /<controller>/
        [Authorize(Roles = UserType.User)]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Api
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
                    Data = await _queryService.FindCouponAsync(id)
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
        public async Task<ResultMessage> GetPage(string title, int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = await _queryService.FindCouponsForPageAsync(title, pageIndex, pageSize)
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> SpiderCoupon([FromBody]string url)
        {
            try
            {
                var spider = new SpiderClient();
                var htmlContent = spider.LoadHtml(url);
                var content = Regex.Match(htmlContent, "<section class=\"bd\">[\\s\\S]*?</section>").Value;
                var coupon = new Coupon();
                coupon.BaseUrl = url;
                var title = Regex.Match(content, "<div class=\"coupon-shop-title\">[\\s\\S]*?</div>").Value;
                title = Regex.Match(title, "<a href=[\\s\\S]*?</a>").Value;
                coupon.Url = Regex.Match(title, "\"[\\s\\S]*?\"").Value.Trim('"');
                coupon.ShopName = Regex.Match(title, ">[\\s\\S]*[?<]").Value.Trim('<').Trim('>');
                coupon.Title = Regex.Match(content, "<dt>[\\s\\S]*?</dt>").Value
                    .Trim("<dt>".ToCharArray()).Trim("</dt>".ToCharArray());
                coupon.RestCount =int.Parse(Regex
                    .Match(content, "<span class=\"rest\">[\\s\\S]*?</span>")
                    .Value
                    .Trim("<span class=\"rest\">".ToCharArray())
                    .Trim("</span>".ToCharArray()));
                var matches = Regex.Matches(content, "<dd>[\\s\\S]*?</dd>");
                coupon.Description = matches[1].Value.Trim("<dd>".ToCharArray()).Trim("</dd>".ToCharArray());
                var dates = matches[2].Value
                    .Trim("<dd>有效期:".ToCharArray())
                    .Trim("</dd>".ToCharArray())
                    .Split('至');
                coupon.StartDate = DateTime.Parse(dates[0]);
                coupon.EndDate = DateTime.Parse(dates[1]);
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = coupon
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Post([FromBody]Coupon coupon)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                coupon.Id = Guid.NewGuid().ToString();
                coupon.AddTime = DateTime.Now;
                coupon.Version = 0;
                var result = await _service.AddAsync(coupon);
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> Put([FromBody]Coupon coupon)
        {
            try
            {
                var result = await _service.UpdateAsync(coupon);
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
        [Route("coupon/delete")]
        [Authorize(Roles = UserType.User)]
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
