using Moq;
using System;
using System.Net;
using System.Linq;
using AGLDeveloperTest.BusinessLayer.Domain;
using AGLDeveloperTest.BusinessLayer.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Repository;
using AGLDeveloperTest.BusinessLayer.Entity;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class PetsTest
    {
        private Mock<IPeopleRepository> peopleRepository;
        private IPeopleService peopleService;
        private IPets petsDomain;
        private const string CatName1 = "Garfield1";
        private const string CatName2 = "Garfield2";
        private const string CatName3 = "Garfield3";

        private Person[] personArray = new[]
        {
                new Person { name = "Bob", gender = "Male", age = "23",
                                    pets = new []
                                    {
                                        new Pet { name = CatName1, type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Adrian", gender = "Female", age = "36",
                                    pets = new []
                                    {
                                        new Pet { name = CatName2, type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Steve", gender = "Male", age = "56",
                                    pets = new []
                                    {
                                        new Pet { name = CatName3, type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Marie", gender = "Female", age = "25",
                                    pets = new []
                                    {
                                        new Pet { name = "GarField", type = PetType.Dog.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Dave", gender = "Male", age = "17" },
        };

        [TestInitialize]
        public void Initialize()
        {
            peopleRepository = new Mock<IPeopleRepository>();
            peopleRepository.Setup(p => p.HttpStatusCode).Returns(HttpStatusCode.OK);
            peopleRepository.Setup(p => p.IsInitialized).Returns(true);
            peopleService = new PeopleService(peopleRepository.Object);
            petsDomain = new Pets(peopleService);
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldReturnCatsGroupedByOwner()
        {
            peopleRepository.Setup(p => p.People).Returns(personArray);
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(OrderBy.Ascending);
            Assert.IsTrue(groupedPets.Success);
            Assert.AreEqual(2, groupedPets.GroupedByOwner.Count());
            Assert.AreEqual(3, groupedPets.GroupedByOwner.SelectMany(g => g).Count());
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCatsGroupedByOwnerGender_ShouldNotReturnCatsGroupedByOwnerForNotFoundUrl()
        {
            peopleRepository.Setup(p => p.People).Returns((Person[]) null);
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(OrderBy.Ascending);
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldReturnCatsGroupedByOwnerByAscending()
        {
            peopleRepository.Setup(p => p.People).Returns(personArray);
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(OrderBy.Ascending);
            var personPetMapList = groupedPets.GroupedByOwner.SelectMany(g => g).ToList();
            Assert.IsTrue(groupedPets.Success);
            Assert.AreEqual(2, groupedPets.GroupedByOwner.Count());
            Assert.AreEqual(3, groupedPets.GroupedByOwner.SelectMany(g => g).Count());
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
            Assert.AreEqual(CatName1, personPetMapList[0].Pet.name);
            Assert.AreEqual(CatName3, personPetMapList[1].Pet.name);
            Assert.AreEqual(CatName2, personPetMapList[2].Pet.name);
        }

        [TestMethod]
        public void GetCatsGroupedByOwnerGender_ShouldReturnCatsGroupedByOwnerByDecending()
        {
            peopleRepository.Setup(p => p.People).Returns(personArray);
            var groupedPets = petsDomain.GetCatsGroupedByOwnerGender(OrderBy.Decending);
            var personPetMapList = groupedPets.GroupedByOwner.SelectMany(g => g).ToList();
            Assert.IsTrue(groupedPets.Success);
            Assert.AreEqual(2, groupedPets.GroupedByOwner.Count());
            Assert.AreEqual(3, groupedPets.GroupedByOwner.SelectMany(g => g).Count());
            Assert.AreEqual(PetType.Cat, groupedPets.PetType);
            Assert.AreEqual(CatName3, personPetMapList[0].Pet.name);
            Assert.AreEqual(CatName1, personPetMapList[1].Pet.name);
            Assert.AreEqual(CatName2, personPetMapList[2].Pet.name);
        }
    }
}
