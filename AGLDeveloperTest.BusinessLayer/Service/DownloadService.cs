using System.Net.Http;
using AGLDeveloperTest.BusinessLayer.Dto;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public class DownloadService : IDownloadService
    {
        public IHttpService HttpService { get; }

        public DownloadService()
        {
            HttpService = new HttpService();
        }

        public DownloadService(IHttpService httpService)
        {
            HttpService = httpService;
        }

        public ReadStringResposeDto ReadStringFromUrl(string url)
        {
            var readStringResposeDto = new ReadStringResposeDto();
            using (var httpResponseMessage = HttpService.GetHttpResponse(url))
            {
                var responseText = WebDownloadHelper.ReadString(httpResponseMessage).Result;
                readStringResposeDto.Success = httpResponseMessage.IsSuccessStatusCode;
                readStringResposeDto.StatusCode = httpResponseMessage.StatusCode;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    readStringResposeDto.ResponseText = responseText;
                }
                else
                {
                    readStringResposeDto.Message = responseText;
                }
            }
            return readStringResposeDto;
        }
    }
}
