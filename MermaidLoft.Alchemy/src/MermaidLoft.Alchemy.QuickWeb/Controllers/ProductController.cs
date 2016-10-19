using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class ProductController : Controller
    {
        ProductQueryService _queryService;
        ProductService _service;
        public ProductController()
        {
            _queryService = new ProductQueryService();
            _service = new ProductService();

        }

        #region VIEW
        // GET: /<controller>/
        [Authorize(Roles = "Users")]
        public IActionResult Index()
        {
            return View();
        }
        
        #endregion

        #region API
        [HttpGet]
        [Authorize(Roles = "Users")]
        public ResultMessage Get(string id)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindProduct(id)
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
        [Authorize(Roles = "Users")]
        public ResultMessage GetPage(string productName, int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = _queryService.FindProductsForPage(productName, pageIndex, pageSize)
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
        [Authorize(Roles = "Users")]
        public ResultMessage Post([FromBody]Product product)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                product.Id = Guid.NewGuid().ToString();
                product.AddTime = DateTime.Now;
                product.Version = 0;
                var result = _service.Add(product);
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
        [Authorize(Roles = "Users")]
        public ResultMessage Put([FromBody]Product product)
        {
            try
            {
                var result = _service.Update(product);
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
        [Route("product/delete")]
        [Authorize(Roles = "Users")]
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
