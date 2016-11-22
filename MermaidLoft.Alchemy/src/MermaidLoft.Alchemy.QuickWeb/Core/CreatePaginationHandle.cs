
namespace MermaidLoft.Alchemy.QuickWeb.Core
{
    public class CreatePaginationHandle
    {
        public const int STARTINDEX = 1;
        //public const int MAXNODESIZE = 1;
        public string Create(int current, int pageSize, int totalPage, int maxNodeSize, string condition)
        {
            var result = string.Empty;
            if (totalPage < 1)
            {
                totalPage = 1;
            }
            result += "<li class='disabled'><a href='#'>上一页</a></li>";
            if (current != STARTINDEX)
            {
                result = "";
                result += "<li><a href='?pageIndex=" +
                    (current-1) + "&pageSize={0}{1}'>上一页</a></li>";
                result += "<li><a href='?pageIndex=1&pageSize={0}{1}'>1</a></li>";
            }
            result+=NodeDelete(current, 1, maxNodeSize, totalPage);
            //当前页
            result += "<li class='active'><a href='?pageIndex=" +
                current + "&pageSize={0}{1}'>" +
                current + "</a></li>";
            result += NodeAdd(current, 1, maxNodeSize, totalPage);
            if (current != totalPage)
            {
                result += "<li><a href='?pageIndex="
                    + totalPage + "&pageSize={0}{1}'>"
                    + totalPage + "</a></li>";
                result += "<li><a href='?pageIndex="
                    + (current + 1) + "&pageSize={0}{1}'>下一页</a></li>";
            }
            else {
                result += "<li class='disabled'><a href='#'>下一页</a></li>";
            }
            return string.Format(result,pageSize,condition);
        }

        public string NodeDelete(int current,int nodeIndex, int maxNodeSize, int totalPage)
        {
            string result = string.Empty;
            if (nodeIndex <= maxNodeSize)
            {
                result+=NodeDelete(current, nodeIndex + 1, maxNodeSize, totalPage);
            }
            else if (current - nodeIndex > STARTINDEX + 1)
            {
                result += "<li><span>... </span></li>";
            }

            if (current - nodeIndex > STARTINDEX)
            {
                result += "<li><a href='?pageIndex=" 
                    + (current - nodeIndex) 
                    + "&pageSize={0}{1}'>" 
                    + (current - nodeIndex) + "</a></li>";
            }
            return result;
        }

        public string NodeAdd(int current, int nodeIndex, int maxNodeSize ,int  totalPage)
        {
            string result = string.Empty;
            if (current + nodeIndex < totalPage)
            {
                result += "<li><a href='?pageIndex=" 
                    + (current + nodeIndex)
                    + "&pageSize={0}{1}'>"
                    + (current + nodeIndex) + "</a></li>";
            }

            if (nodeIndex <= maxNodeSize)
            {
                result += NodeAdd(current, nodeIndex + 1, maxNodeSize, totalPage);
            }
            else if (current + nodeIndex < totalPage - 1)
            {
                result += "<li><span>... </span></li>";
            }
            return result;
        }
    }
}
