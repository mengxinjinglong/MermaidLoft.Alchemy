using System;

namespace MermaidLoft.Alchemy.BaseDomain.BaseResourceDomain
{
    public class BaseResource
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime EditTime { get; set; }
    }
}
