using System.Net;
using AGLDeveloperTest.BusinessLayer.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class HttpServiceTests
    {
        private const string PeoplePetsJsonUrl = "http://agl-developer-test.azurewebsites.net/people.json";
        private IHttpService httpService;

        [TestInitialize]
        public void Initialize()
        {
            httpService = new HttpService();
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnValidHttpResponseMessage()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse(null))
            {
                Assert.IsNotNull(httpResponseMessage);
            }
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnBadRequestForNullUrl()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse(null))
            {
                Assert.IsNotNull(httpResponseMessage);
                Assert.AreEqual(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
                Assert.AreEqual(false, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnBadRequestForEmptyUrl()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse(string.Empty))
            {
                Assert.IsNotNull(httpResponseMessage);
                Assert.AreEqual(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
                Assert.AreEqual(false, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnBadRequestForInvalidUrl()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse("1"))
            {
                Assert.IsNotNull(httpResponseMessage);
                Assert.AreEqual(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
                Assert.AreEqual(false, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnNotFoundForUrlNotFound()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse(PeoplePetsJsonUrl + "1"))
            {
                Assert.IsNotNull(httpResponseMessage);
                Assert.AreEqual(HttpStatusCode.NotFound, httpResponseMessage.StatusCode);
                Assert.AreEqual(false, httpResponseMessage.IsSuccessStatusCode);
            }
        }

        [TestMethod]
        public void GetHttpResponse_ShouldReturnOkForValidUrl()
        {
            using (var httpResponseMessage = httpService.GetHttpResponse(PeoplePetsJsonUrl))
            {
                Assert.IsNotNull(httpResponseMessage);
                Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);
                Assert.AreEqual(true, httpResponseMessage.IsSuccessStatusCode);
            }
        }
    }
}
