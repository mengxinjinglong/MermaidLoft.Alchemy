
using MermaidLoft.Alchemy.QuickWeb.Spider;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Core
{
    public class BaseResources
    {
        private static readonly BaseResources _instance = new BaseResources();
        public static BaseResources Instance
        {
            get{
                return _instance;
            }
        }
        public string[] ProduceTypes { get; set; }
        public BaseResources()
        {
            #region
            ProduceTypes = new string[]{ "保健食品/膳食营养补充食品",
                "女士内衣/男士内衣/家居服",
                "女装/女士精品",
                "流行男鞋",
                "彩妆/香水/美妆工具",
                "饰品/流行首饰/时尚饰品新",
                "收纳整理",
                "童装/婴儿装/亲子装",
                "女鞋",
                "3C数码配件",
                "零食/坚果/特产",
                "床上用品",
                "洗护清洁剂/卫生巾/纸/香薰",
                "生活电器",
                "男装",
                "个人护理/保健/按摩器材",
                "玩具/童车/益智/积木/模型",
                "厨房电器",
                "童鞋/婴儿鞋/亲子鞋",
                "美容护肤/美体/精油",
                "电子/电工",
                "箱包皮具/热销女包/男包",
                "节庆用品/礼品",
                "酒类",
                "粮油米面/南北干货/调味品",
                "尿片/洗护/喂哺/推车床",
                "居家日用",
                "传统滋补营养品",
                "宠物/宠物食品及用品",
                "电子词典/电纸书/文化用品",
                "家庭/个人清洁工具",
                "咖啡/麦片/冲饮",
                "居家布艺",
                "办公设备/耗材/相关服务",
                "住宅家具",
                "厨房/烹饪用具",
                "餐饮具",
                "智能设备",
                "户外/登山/野营/旅行用品",
                "书籍/杂志/报纸",
                "运动/瑜伽/健身/球迷用品",
                "鲜花速递/花卉仿真/绿植园艺",
                "手表",
                "水产肉类/新鲜蔬果/熟食",
                "影音电器",
                "家装主材",
                "服饰配件/皮带/帽子/围巾",
                "汽车/用品/配件/改装",
                "运动包/户外包/配件",
                "大家电",
                "OTC药品/医疗器械/计生用品",
                "基础建材",
                "茶",
                "隐形眼镜/护理液",
                "家居饰品",
                "电动车/配件/交通工具",
                "美发护发/假发",
                "个性定制/设计服务/DIY",
                "特色手工艺",
                "孕妇装/孕产妇用品/营养",
                "ZIPPO/瑞士军刀/眼镜",
                "网络设备/网络相关",
                "运动鞋new",
                "成人用品/情趣用品",
                "包装",
                "DIY电脑",
                "五金/工具",
                "运动服/休闲服装",
                "电子元器件市场",
                "自行车/骑行装备/零配件",
                "乐器/吉他/钢琴/配件",
                "珠宝/钻石/翡翠/黄金",
                "电脑硬件/显示器/电脑周边",
                "腾讯QQ专区",
                "奶粉/辅食/营养品/零食",
                "闪存卡/U盘/存储/移动硬盘",
                "MP3/MP4/iPod/录音笔",
                "商业/办公家具",
                "模玩/动漫/周边/cos/桌游",
                "精制中药材",
                "摩托车/装备/配件",
                "手机",
                };
            #endregion

            #region 普通搜索排序
            if (NormalSortTypes == null)
            {
                NormalSortTypes = new List<SortTypeKeyValue>();
            }
            var selectedKeyValue = new SortTypeKeyValue { Key = "默认", Value = new Dictionary<string, string> { { "queryType", "0" } } };
            NormalSortTypes.Add(selectedKeyValue);
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "人气", Value = new Dictionary<string, string> { { "queryType", "2" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "价格从高到低", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "3" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "价格从低到高", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "4" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "销量", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "9" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "收入比率", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "1" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "月推广量", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "5" } } });
            NormalSortTypes.Add(new SortTypeKeyValue { Key = "月支出佣金", Value = new Dictionary<string, string> { { "queryType", "0" }, { "sortType", "7" } } });
            #endregion
            #region 高佣搜索排序
            if (HighCommissionSortTypes == null)
            {
                HighCommissionSortTypes = new List<SortTypeKeyValue>();
            }
            var keyValue = new SortTypeKeyValue { Key = "默认", Value = new Dictionary<string, string> { { "sortType", "" } } };
            HighCommissionSortTypes.Add(keyValue);
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "价格从高到低", Value = new Dictionary<string, string> { { "sortType", "3" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "价格从低到高", Value = new Dictionary<string, string> { { "sortType", "4" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "销量从高到低", Value = new Dictionary<string, string> { { "sortType", "9" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "收入比率从高到低", Value = new Dictionary<string, string> { { "sortType", "1" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "月推广量从高到低", Value = new Dictionary<string, string> { { "sortType", "5" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "月支出佣金从高到低", Value = new Dictionary<string, string> { { "sortType", "7" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "活动开始时间从近到远", Value = new Dictionary<string, string> { { "sortType", "13" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "活动剩余时间从长到短", Value = new Dictionary<string, string> { { "sortType", "11" } } });
            HighCommissionSortTypes.Add(new SortTypeKeyValue { Key = "活动剩余时间从短到长", Value = new Dictionary<string, string> { { "sortType", "12" } } });
            #endregion
        }

        public List<SortTypeKeyValue> NormalSortTypes { get; set; }
        public List<SortTypeKeyValue> HighCommissionSortTypes { get; set; }
    }
}
