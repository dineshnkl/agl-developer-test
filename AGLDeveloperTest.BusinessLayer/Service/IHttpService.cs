using System.Net.Http;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public interface IHttpService
    {
        HttpResponseMessage GetHttpResponse(string url);
    }
}
