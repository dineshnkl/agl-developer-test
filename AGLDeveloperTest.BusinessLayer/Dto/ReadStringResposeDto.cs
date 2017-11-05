using System.Net;

namespace AGLDeveloperTest.BusinessLayer.Dto
{
    public class ReadStringResposeDto : ResponseDto
    {
        public string ResponseText { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
