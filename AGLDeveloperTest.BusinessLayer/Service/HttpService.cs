using System.Net.Http;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public class HttpService : IHttpService
    {
        public HttpResponseMessage GetHttpResponse(string url)
        {
            return WebDownloadHelper.Get(url).Result;
        }
        
    }
}
