using Infrastructure.Spider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace MermaidLoft.Alchemy.QuickWeb.Spider
{
    public class SpiderRespository
    {
        public async Task<Data> Search(string url,SearchCondition condition)
        {
            var paramsDirectory = new Dictionary<string, string>
                {
                    {"q",condition.KeyWord},
                    { "startBiz30day",condition.StartBiz30day},
                    { "startTkRate",condition.StartTkRate},
                    { "endTkRate",condition.EndTkRate},
                    { "startPrice",condition.StartPrice},
                    { "endPrice",condition.EndPrice},
                    { "dpyhq",condition.dpyhq=="1"?"1&shopTag=dpyhq":""},
                    //{ "freeShipment",condition.freeShipment?"1":""},
                    //{ "dxjh",condition.dxjh?"1":""},
                    //{ "hPayRate30",condition.hPayRate30?"1":""},
                    { "b2c",condition.b2c?"1":""},
                    {"perPageSize", condition.PageSize.ToString()}, //最大100
                    {"toPage", condition.ToPage.ToString()}, //查看第几页内容
                    //佣金比例1%-10%：startTkRate=1&endTkRate=10
                    //价格100-200元：startPrice=100&endPrice=200
                    //月销售量1000以上：startBiz30day=1000
                };
            SortTypeKeyValue selectedKeyValue;
            if (condition.SpiderType == EnumSpiderType.Normal)
            {
                paramsDirectory.Add("freeShipment", condition.freeShipment ? "1" : "");
                paramsDirectory.Add("dxjh", condition.dxjh ? "1" : "");
                paramsDirectory.Add("hPayRate30", condition.hPayRate30 ? "1" : "");
                selectedKeyValue = Core.BaseResources.Instance.NormalSortTypes
                    .FirstOrDefault(item=>item.Key==condition.SortTypeName);
            }
            else
            {
                paramsDirectory.Add("channel", "qqhd"); //高拥活动
                                                        //paramsDirectory.Add("sortType", ""); //排序
                paramsDirectory.Add("userType", condition.userType.ToString()); //淘宝：0，天猫：1
                paramsDirectory.Add("jpmj", condition.jpmj ? "1" : "");//金牌卖家
                selectedKeyValue = Core.BaseResources.Instance.HighCommissionSortTypes
                    .FirstOrDefault(item => item.Key == condition.SortTypeName);
            }

            foreach (var item in selectedKeyValue?.Value)
            {
                if (!paramsDirectory.Keys.Contains(item.Key))
                {
                    paramsDirectory.Add(item.Key, item.Value);
                }
            }

            var paramsContent = string.Empty;
            foreach (var item in paramsDirectory)
            {
                if(!string.IsNullOrEmpty(item.Value))
                {
                    paramsContent += item.Key + "=" + item.Value + "&";
                }
            }
            paramsContent = paramsContent.TrimEnd('&');
            var spider = new SpiderClient();
            var htmlContent = spider.LoadHtml(url+"?"+paramsContent);
            return JsonConvert.DeserializeObject<Data>(htmlContent);
        }
    }
}
