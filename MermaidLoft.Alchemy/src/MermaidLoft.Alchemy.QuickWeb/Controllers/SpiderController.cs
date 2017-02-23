using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MermaidLoft.Alchemy.QuickWeb.Core;
using MermaidLoft.Alchemy.QuickWeb.Spider;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MermaidLoft.Alchemy.QuickWeb.Controllers
{
    public class SpiderController : Controller
    {
        #region Views
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Web Api
        [HttpPost]
        public async Task<ResultMessage> Grab([FromBody] SearchCondition condition)
        {//[FromBody]SearchCondition condition
            try
            {
                var url = "http://pub.alimama.com/items/search.json";
                //SearchCondition condition = new SearchCondition
                //{
                //    SpiderType = EnumSpiderType.Normal,
                //    PageSize = 100,
                //    ToPage = 1,
                //    SortTypeName = "默认",
                //};
                SpiderRespository spider = new SpiderRespository();
                return new ResultMessage
                {
                    Success = true,
                    Status = EnumStatus.Success,
                    Data = await spider.Search(url, condition)
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
