using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Spider
{
    public class Data
    {
        [JsonProperty("data")]
        public DataItem DataItems { get; set; }
    }

    public class DataItem
    {
        [JsonProperty("head")]
        public object Head { get; set; }
        [JsonProperty("paginator")]
        public object Paginator { get; set; }

        [JsonProperty("pageList")]
        public List<PageItem> PageList { get; set; }
    }

    public class PageItem
    {
        /// <summary>
        /// 店名
        /// </summary>
        [JsonProperty("shopTitle")]
        public string shopTitle { get; set; }
        /// <summary>
        /// 原价格
        /// </summary>
        [JsonProperty("reservePrice")]
        public string reservePrice { get; set; }
        /// <summary>
        /// 促销价格
        /// </summary>
        [JsonProperty("zkPrice")]
        public string zkPrice { get; set; }

        public string DiscountPrice
        {
            get
            {
                var discount = double.Parse(zkPrice) * 10 / double.Parse(reservePrice);
                return discount.ToString("F");
            }
        }

        private string _title;
        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("title")]
        public string title
        {
            get { return _title; }
            set
            {
                if (value == null) return;
                _title = value.Replace("<span class=H>", "").Replace("</span>", "");
            }
        }
        /// <summary>
        /// 链接
        /// </summary>
        [JsonProperty("auctionUrl")]
        public string auctionUrl { get; set; }
        /// <summary>
        /// 收入比率
        /// </summary>
        [JsonProperty("tkRate")]
        public string tkRatea { get; set; }
        /// <summary>
        /// 佣金
        /// </summary>
        [JsonProperty("tkCommFee")]
        public string tkCommFee { get; set; }
        /// <summary>
        /// 月推广量
        /// </summary>
        [JsonProperty("totalNum")]
        public string totalNum { get; set; }
        /// <summary>
        /// 月支出佣金
        /// </summary>
        [JsonProperty("totalFee")]
        public string totalFee { get; set; }
        /// <summary>
        /// 月销售量
        /// </summary>
        [JsonProperty("biz30day")]
        public string biz30day { get; set; }
        /// <summary>
        /// 剩余天数
        /// </summary>
        [JsonProperty("dayLeft")]
        public string dayLeft
        {
            get { return _dayLeft; }
            set
            {
                if (value.Contains("-"))
                    _dayLeft = "0";
                else
                    _dayLeft = value;
            }
        }
        public string _dayLeft { get; set; }
        [JsonProperty("eventRate")]
        public string eventRate { get; set; }
        [JsonProperty("pictUrl")]
        public string pictUrl { get; set; }
        [JsonProperty("sellerId")]
        public string sellerId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty("nick")]
        public string nick { get; set; }
        /// <summary>
        /// 优惠券总数
        /// </summary>
        [JsonProperty("couponTotalCount")]
        public int couponTotalCount { get; set; }
        /// <summary>
        /// 优惠券剩余数
        /// </summary>
        [JsonProperty("couponLeftCount")]
        public int couponLeftCount { get; set; }
        /// <summary>
        /// 优惠券金额
        /// </summary>
        [JsonProperty("couponAmount")]
        public int couponAmount { get; set; }
        /// <summary>
        /// 优惠券描述
        /// </summary>
        [JsonProperty("couponInfo")]
        public string couponInfo { get; set; }
        /// <summary>
        /// 生效金额
        /// </summary>
        [JsonProperty("couponStartFee")]
        public float couponStartFee { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("couponEffectiveStartTime")]
        public string couponEffectiveStartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("couponEffectiveEndTime")]
        public string couponEffectiveEndTime { get; set; }
    }

}
