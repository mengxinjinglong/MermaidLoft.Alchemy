using System;

namespace MermaidLoft.Alchemy.BaseDomain.ArticleDomain
{
    public class Article
    {
        public string Id { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string AuthorId { get; set; }
        public string Tags { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime EditTime { get; set; }
        public long Hits { get; set; }
        public int Status { get; set; }
    }
}
