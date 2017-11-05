using AGLDeveloperTest.BusinessLayer.Dto;
using System.Net.Http;

namespace AGLDeveloperTest.BusinessLayer.Service
{
    public interface IDownloadService
    {
        ReadStringResposeDto ReadStringFromUrl(string url);
    }
}
