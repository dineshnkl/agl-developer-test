using System.Net;
using System.Net.Http;

namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class ReadStringResposeDto : ResponseDto
    {
        public string ResponseText { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
