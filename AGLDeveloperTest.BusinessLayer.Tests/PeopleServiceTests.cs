using Moq;
using AGLDeveloperTest.BusinessLayer.Entity;
using AGLDeveloperTest.BusinessLayer.Enums;
using AGLDeveloperTest.BusinessLayer.Repository;
using AGLDeveloperTest.BusinessLayer.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Linq;
using System;
using System.Collections.Generic;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class PeopleServiceTests
    {
        private Mock<IPeopleRepository> peopleRepository;
        private IPeopleService peopleService;

        private Person[] personArray = new[]
        {
                new Person { name = "Bob", gender = "Male", age = "23",
                                    pets = new []
                                    {
                                        new Pet { name = "Garfield", type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Adrian", gender = "Female", age = "36",
                                    pets = new []
                                    {
                                        new Pet { name = "Garfield", type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Steve", gender = "Male", age = "56",
                                    pets = new []
                                    {
                                        new Pet { name = "Garfield", type = PetType.Cat.ToString() },
                                        new Pet { name = "Fido", type = PetType.Dog.ToString() },
                                    }
                },
                new Person { name = "Marie", gender = "Female", age = "25",
                                    pets = new []
                                    {
                                        new Pet { name = "Garfield", type = PetType.Dog.ToString() },
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
        }

        [TestMethod]
        public void GetCatsWithOwner_ShouldReturnCatsWithOwner()
        {
            peopleRepository.Setup(p => p.People).Returns(personArray);
            var catsWithOwner = peopleService.GetCatsWithOwner();
            peopleRepository.Verify(p => p.People, Times.Once);
            Assert.AreEqual(3, catsWithOwner.Count());
        }

        [TestMethod]
        public void GetCatsWithOwner_ShouldReturnZeroCatsWithOwner()
        {
            peopleRepository.Setup(p => p.People).Returns(new Person[] { });
            var catsWithOwner = peopleService.GetCatsWithOwner();
            peopleRepository.Verify(p => p.People, Times.Once);
            Assert.AreEqual(0, catsWithOwner.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCatsWithOwner_ShouldThrowNullReferenceExceptionIfDataIsNull()
        {
            peopleRepository.Setup(p => p.People).Returns(null as IEnumerable<Person>);
            var catsWithOwner = peopleService.GetCatsWithOwner();
        }

        [TestMethod]
        public void GetPetGroupsGroupedByOwnerGender_ShouldReturnOrderedPets()
        {
            var personPetMapArray = new PersonPetMap[] {
                new PersonPetMap {Person=new Person { age = "33", gender = "Male", name = "Harvey" },
                                  Pet = new Pet { name = "Garfield", type = PetType.Cat.ToString() } },
                new PersonPetMap {Person=new Person { age = "33", gender = "Female", name = "Sophie" },
                                  Pet = new Pet { name = "Hissy", type = PetType.Cat.ToString() } },
            };
            var petsGroupedOwnerGender = peopleService.GetPetGroupsGroupedByOwnerGender(personPetMapArray, OrderBy.Ascending);
            Assert.AreEqual(2, petsGroupedOwnerGender.Count());
            Assert.AreEqual(2, petsGroupedOwnerGender.SelectMany(g => g).Count());
        }

        [TestMethod]
        public void GetPetGroupsGroupedByOwnerGender_ShouldReturnOrderedPetsForNullGender()
        {
            var personPetMapArray = new PersonPetMap[] {
                new PersonPetMap {Person=new Person { age = "33", gender = null, name = "Harvey" },
                                  Pet = new Pet { name = "Garfield", type = PetType.Cat.ToString() } },
                new PersonPetMap {Person=new Person { age = "33", gender = "Female", name = "Sophie" },
                                  Pet = new Pet { name = "Hissy", type = PetType.Cat.ToString() } },
            };
            var petsGroupedOwnerGender = peopleService.GetPetGroupsGroupedByOwnerGender(personPetMapArray, OrderBy.Ascending);
            Assert.AreEqual(2, petsGroupedOwnerGender.Count());
            Assert.AreEqual(2, petsGroupedOwnerGender.SelectMany(g => g).Count());
        }
    }
}
