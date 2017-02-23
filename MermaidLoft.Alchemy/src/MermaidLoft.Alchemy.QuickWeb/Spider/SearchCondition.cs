using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidLoft.Alchemy.QuickWeb.Spider
{
    public class SearchCondition
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 月销售笔及以上
        /// </summary>
        public string StartBiz30day { get; set; }
        /// <summary>
        /// 收入比率开始
        /// </summary>
        public string StartTkRate { get; set; }
        /// <summary>
        /// 收入比率结束
        /// </summary>
        public string EndTkRate { get; set; }
        /// <summary>
        /// 价格开始
        /// </summary>
        public string StartPrice { get; set; }
        /// <summary>
        /// 价格结束
        /// </summary>
        public string EndPrice { get; set; }
        /// <summary>
        /// 查看第几页数
        /// </summary>
        public int ToPage { get; set; }
        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
        ///// <summary>
        ///// 状态
        ///// </summary>
        //public string Status { get; set; }
        /// <summary>
        /// 包邮
        /// </summary>
        public bool freeShipment { get; set; }
        /// <summary>
        /// 定向计划
        /// </summary>
        public bool dxjh { get; set; }
        /// <summary>
        /// 月成交转化率高于行业均值
        /// </summary>
        public bool hPayRate30 { get; set; }
        /// <summary>
        /// 天猫旗舰店
        /// </summary>
        public bool b2c { get; set; }
        /// <summary>
        /// 0:淘宝,1:天猫
        /// </summary>
        public int userType { get; set; }
        /// <summary>
        /// 金牌卖家
        /// </summary>
        public bool jpmj { get; set; }
        public string LastDays { get; set; }

        public string SortTypeName { get; set; }
        public Dictionary<string, string> SortTypeDictionary { get; set; }
        /// <summary>
        /// 折扣开始
        /// </summary>
        public string StartDiscount { get; set; }
        /// <summary>
        /// 折扣结束
        /// </summary>
        public string EndDiscount { get; set; }
        public string StartTotalNum { get; set; }
        //public SearchCondition()
        //{
        //    //Status = "未执行";
        //}

        public EnumSpiderType SpiderType { get; set; }
        public string dpyhq { get; set; }
    }

    public enum EnumSpiderType
    {
        Normal,//普通
        HighCommission,//高佣金
    }
}
