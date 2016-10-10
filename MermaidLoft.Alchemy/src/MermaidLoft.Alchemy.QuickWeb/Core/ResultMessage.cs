using System;

namespace MermaidLoft.Alchemy.QuickWeb.Core
{
    public class ResultMessage
    {
        public bool Success { get; set; }
        public EnumStatus Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }

        public ResultMessage()
        {
            Success = true;
            Status = EnumStatus.Success;
        }
    }
    public enum EnumStatus {
        Success=0,
        Failure=1,
    }
}
