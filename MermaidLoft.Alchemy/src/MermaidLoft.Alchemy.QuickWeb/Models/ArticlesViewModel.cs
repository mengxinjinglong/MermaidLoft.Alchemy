using MermaidLoft.Alchemy.BaseDomain.ArticleDomain;
using MermaidLoft.Alchemy.QuickWeb.Core;
using System.Collections.Generic;

namespace MermaidLoft.Alchemy.QuickWeb.Models
{
    public class ArticlesViewModel
    {
        public string Key { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Article> Articles { get; set; }

        public string Pagination
        {
            get
            {
                return new CreatePaginationHandle()
                    .Create(PageIndex, PageSize,
                    (TotalCount +PageSize- 1) / PageSize,
                    3,
                    string.Format("&key={0}", Key));
            }
        }
    }
}
