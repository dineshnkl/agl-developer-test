using Moq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGLDeveloperTest.BusinessLayer.Service;
using System.Net;
using AGLDeveloperTest.BusinessLayer.Dto;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class DownloadServiceTests
    {
        private const string ValidUrl = "validurl";
        private const string NotFoundUrl = "notfoundurl";
        private IDownloadService downloadService;
        private Mock<IHttpService> httpService;
        private HttpResponseMessage nullHttpResponseMessage;
        private HttpResponseMessage emptyUrlHttpResponseMessage;
        private HttpResponseMessage urlNotFoundHttpResponseMessage;
        private HttpResponseMessage validUrlHttpResponse;

        private const string BadRequest = "BadRequest";
        private const string NotFound = "NotFound";

        private PersonJsonDto[] validUrlPersonArray = new[]
        {
                new PersonJsonDto { name = "Bob", gender = "Male", age = "23",
                                    pets = new []
                                    {
                                        new PetJsonDto { name = "Garfield", type = PetType.Cat.ToString() },
                                        new PetJsonDto { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                }
        };
        private string validUrlJsonMessage;

        [TestInitialize]
        public void Initialize()
        {
            httpService = new Mock<IHttpService>();
            validUrlJsonMessage = JsonHelper.Serialize(validUrlPersonArray);
            nullHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(BadRequest) };
            emptyUrlHttpResponseMessage = nullHttpResponseMessage;
            urlNotFoundHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent(NotFound) };
            validUrlHttpResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(validUrlJsonMessage) };
            httpService.Setup(h => h.GetHttpResponse(null)).Returns(nullHttpResponseMessage);
            httpService.Setup(h => h.GetHttpResponse(string.Empty)).Returns(emptyUrlHttpResponseMessage);
            httpService.Setup(h => h.GetHttpResponse(NotFoundUrl)).Returns(urlNotFoundHttpResponseMessage);
            httpService.Setup(h => h.GetHttpResponse(ValidUrl)).Returns(validUrlHttpResponse);
            downloadService = new DownloadService(httpService.Object);
        }

        [TestMethod]
        public void ReadStringFromUrl_ShouldReturnValidResponseDto()
        {
            var readStringResponseDto = downloadService.ReadStringFromUrl(null);
            Assert.IsNotNull(readStringResponseDto);
            httpService.Verify(h => h.GetHttpResponse(null));
        }

        [TestMethod]
        public void ReadStringFromUrl_ShouldNotReturnDataForNullUrl()
        {
            var readStringResponseDto = downloadService.ReadStringFromUrl(null);
            Assert.IsNotNull(readStringResponseDto);
            Assert.IsFalse(readStringResponseDto.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest, readStringResponseDto.StatusCode);
            Assert.IsNull(readStringResponseDto.ResponseText);
            Assert.AreEqual(BadRequest, readStringResponseDto.Message);
            httpService.Verify(h => h.GetHttpResponse(null));
        }

        [TestMethod]
        public void ReadStringFromUrl_ShouldNotReturnDataForEmptyUrl()
        {
            var readStringResponseDto = downloadService.ReadStringFromUrl(string.Empty);
            Assert.IsNotNull(readStringResponseDto);
            Assert.IsFalse(readStringResponseDto.Success);
            Assert.AreEqual(HttpStatusCode.BadRequest, readStringResponseDto.StatusCode);
            Assert.IsNull(readStringResponseDto.ResponseText);
            Assert.AreEqual(BadRequest, readStringResponseDto.Message);
            httpService.Verify(h => h.GetHttpResponse(string.Empty));
        }

        [TestMethod]
        public void ReadStringFromUrl_ShouldNotReturnDataForUrlNotFound()
        {
            var readStringResponseDto = downloadService.ReadStringFromUrl(NotFoundUrl);
            Assert.IsNotNull(readStringResponseDto);
            Assert.IsFalse(readStringResponseDto.Success);
            Assert.AreEqual(HttpStatusCode.NotFound, readStringResponseDto.StatusCode);
            Assert.IsNull(readStringResponseDto.ResponseText);
            Assert.AreEqual(NotFound, readStringResponseDto.Message);
            httpService.Verify(h => h.GetHttpResponse(NotFoundUrl));
        }

        [TestMethod]
        public void ReadStringFromUrl_ShouldReturnDataForValidUrl()
        {
            var readStringResponseDto = downloadService.ReadStringFromUrl(ValidUrl);
            Assert.IsNotNull(readStringResponseDto);
            Assert.IsTrue(readStringResponseDto.Success);
            Assert.AreEqual(HttpStatusCode.OK, readStringResponseDto.StatusCode);
            Assert.AreEqual(validUrlJsonMessage, readStringResponseDto.ResponseText);
            Assert.IsNull(readStringResponseDto.Message);
            httpService.Verify(h => h.GetHttpResponse(ValidUrl));
        }
    }
}
