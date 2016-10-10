using System;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.BaseDomain.CouponDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Infrastructure.Spider;
using System.Text.RegularExpressions;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class CouponController : Controller
    {
        CouponQueryService _queryService;
        CouponService _service;
        public CouponController()
        {
            _queryService = new CouponQueryService();
            _service = new CouponService();

        }

        #region View
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Api
        [HttpGet]
        public ResultMessage Get(string id)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindCoupon(id)
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
        public ResultMessage GetPage(string title, int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindCouponsForPage(title, pageIndex, pageSize)
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
        public ResultMessage SpiderCoupon([FromBody]string url)
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
        public ResultMessage Post([FromBody]Coupon coupon)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                coupon.Id = Guid.NewGuid().ToString();
                coupon.AddTime = DateTime.Now;
                coupon.Version = 0;
                var result = _service.Add(coupon);
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
        public ResultMessage Put([FromBody]Coupon coupon)
        {
            try
            {
                var result = _service.Update(coupon);
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
}
