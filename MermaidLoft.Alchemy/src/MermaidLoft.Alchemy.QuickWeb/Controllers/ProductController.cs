using MermaidLoft.Alchemy.BaseDomain.ProductDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductQueryService _queryService;
        private readonly IProductService _service;
        private readonly IAuthorizeValidator _authorizeValidator;
        public ProductController(IProductQueryService queryService, 
            IProductService service,
            IAuthorizeValidator authorizeValidator)
        {
            _queryService = queryService;
            _service = service;
            _authorizeValidator = authorizeValidator;
        }

        #region VIEW
        // GET: /<controller>/
        [Authorize(Roles = UserType.User)]
        public IActionResult Index()
        {
            return View();
        }
        
        #endregion

        #region API
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
                    Data = await _queryService.FindProductAsync(id)
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
        [Authorize(Roles = UserType.User)]
        public async Task<ResultMessage> GetPage(string productName, int pageIndex, int pageSize)
        {
            try
            {
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = await _queryService.FindProductsForPageAsync(productName, pageIndex, pageSize)
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
        public async Task<ResultMessage> Post([FromBody]Product product)
        {
            //没有添加[FromBody]，无法获取到user内容，user值为默认值 as user = new User();
            try
            {
                product.Id = Guid.NewGuid().ToString();
                product.AddTime = DateTime.Now;
                product.Version = 0;
                var result = await _service.AddAsync(product);
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
        public async Task<ResultMessage> Put([FromBody]Product product)
        {
            try
            {
                var result = await _service.UpdateAsync(product);
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
