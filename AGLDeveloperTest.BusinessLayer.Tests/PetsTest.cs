using Moq;
using System.Net;
using System.Linq;
using AGLDeveloperTest.BusinessLayer.Domain;
using AGLDeveloperTest.BusinessLayer.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGLDeveloperTest.BusinessLayer.Dto;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class PetsTest
    {
        private Mock<IDownloadService> downloadService;
        private IPets petsDomain;

        private const string ValidUrl = "validurl";
        private PersonJsonDto[] validUrlPersonArray = new[]
        {
                new PersonJsonDto { name = "Bob", gender = "Male", age = "23",
                                    pets = new []
                                    {
                                        new PetJsonDto { name = "Garfield", type = PetType.Cat.ToString() },
                                        new PetJsonDto { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new PersonJsonDto { name = "Adrian", gender = "Female", age = "36",
                                    pets = new []
                                    {
                                        new PetJsonDto { name = "Garfield", type = PetType.Cat.ToString() },
                                        new PetJsonDto { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new PersonJsonDto { name = "Steve", gender = "Male", age = "56",
                                    pets = new []
                                    {
                                        new PetJsonDto { name = "Garfield", type = PetType.Cat.ToString() },
                                        new PetJsonDto { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new PersonJsonDto { name = "Marie", gender = "Female", age = "25",
                                    pets = new []
                                    {
                                        new PetJsonDto { name = "Garfield", type = PetType.Dog.ToString() },
                                        new PetJsonDto { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new PersonJsonDto { name = "Dave", gender = "Male", age = "17" },
        };
        private string validUrlPersonJson;
        private ReadStringResposeDto validUrlReadStringResponse;

        private const string NotFound = "NotFound";
        private const string NotFoundUrl = "notfoundurl";
        private ReadStringResposeDto notFoundUrlReadStringResponse;

        private const string BadRequest = "BadRequest";
        private const string BadUrl = "badurl";
        private ReadStringResposeDto badUrlReadStringResponse;

        [TestInitialize]
        public void Initialize()
        {
            downloadService = new Mock<IDownloadService>();
            validUrlPersonJson = JsonHelper.Serialize(validUrlPersonArray);
            validUrlReadStringResponse = new ReadStringResposeDto { Success = true, StatusCode = HttpStatusCode.OK, ResponseText = validUrlPersonJson };
            notFoundUrlReadStringResponse = new ReadStringResposeDto { Success = false, StatusCode = HttpStatusCode.NotFound, Message = NotFound };
            badUrlReadStringResponse = new ReadStringResposeDto { Success = false, StatusCode = HttpStatusCode.BadRequest, Message = BadRequest };
            downloadService.Setup(s => s.ReadStringFromUrl(ValidUrl)).Returns(validUrlReadStringResponse);
            downloadService.Setup(s => s.ReadStringFromUrl(NotFoundUrl)).Returns(notFoundUrlReadStringResponse);
            downloadService.Setup(s => s.ReadStringFromUrl(BadUrl)).Returns(badUrlReadStringResponse);
            petsDomain = new Pets(downloadService.Object);
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldReturnCatsGroupedByOwnerForValidUrl()
        {
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(ValidUrl, OrderBy.Ascending);
            Assert.IsTrue(groupedPets.Success);
            Assert.AreEqual(validUrlPersonArray.Length, groupedPets.People.Count());
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
            Assert.AreEqual(2, groupedPets.GroupedPets.Count());
            Assert.AreEqual(3, groupedPets.GroupedPets.SelectMany(g => g.Pets).Count());
            downloadService.Verify(s => s.ReadStringFromUrl(ValidUrl));
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldNotReturnCatsGroupedByOwnerForNotFoundUrl()
        {
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(NotFoundUrl, OrderBy.Ascending);
            Assert.IsFalse(groupedPets.Success);
            Assert.IsNull(groupedPets.GroupedPets);
            Assert.IsNull(groupedPets.People);
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
            Assert.AreEqual(NotFound, groupedPets.Message);
            downloadService.Verify(s => s.ReadStringFromUrl(NotFoundUrl));
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldNotReturnCatsGroupedByOwnerForBadUrl()
        {
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(BadUrl, OrderBy.Ascending);
            Assert.IsFalse(groupedPets.Success);
            Assert.IsNull(groupedPets.GroupedPets);
            Assert.IsNull(groupedPets.People);
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
            Assert.AreEqual(BadRequest, groupedPets.Message);
            downloadService.Verify(s => s.ReadStringFromUrl(BadUrl));
        }

    }
}
