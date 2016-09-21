using System.Net;
using System.Net.Http;
using System.Text;

namespace Infrastructure.Spider
{
    public class SpiderClient
    {
        public string LoadHtml(string url,string encodingName= "UTF-8",int timeout = 5000)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, url);
            message.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en-US;q=0.5,en;q=0.3");
            message.Headers.Add("ContentType", "application /x-www-form-urlencoded; charset=UTF-8");
            message.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            HttpClient client = new HttpClient(new GlobalRedirectHandler(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                UseProxy = true,
                Proxy = null
            }));
            var requestTask = client.SendAsync(message);
            requestTask.Wait(timeout);
            var response = requestTask.Result;
            response.EnsureSuccessStatusCode();
            byte[] contentBytes = response.Content.ReadAsByteArrayAsync().Result;
            Encoding htmlCharset = Encoding.GetEncoding(encodingName);
            return htmlCharset.GetString(contentBytes);
        }
    }
}
