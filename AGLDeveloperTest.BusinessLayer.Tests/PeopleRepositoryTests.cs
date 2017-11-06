using System;
using System.Net;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AGLDeveloperTest.BusinessLayer.Repository;

namespace AGLDeveloperTest.BusinessLayer.Tests
{
    [TestClass]
    public class PeopleRepositoryTests
    {
        private IPeopleRepository peopleRepository;

        [TestInitialize]
        public void Initialize()
        {
            peopleRepository = new PeopleRepository();
        }

        [TestMethod]
        public void PeopleRepository_ShouldNotBeInitializedWithoutAccessingPeopleProperty()
        {
            Assert.IsFalse(peopleRepository.IsInitialized);
        }

        [TestMethod]
        public void PeopleRepository_ShouldBeInitializedAfterAccessingPeopleProperty()
        {
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNotNull(peopleRepository.People);
            Assert.IsTrue(peopleRepository.IsInitialized);
        }

        [TestMethod]
        public void PeopleRepository_ShouldNotBeEmpty()
        {
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsTrue(peopleRepository.People.Any());
            Assert.IsTrue(peopleRepository.IsInitialized);
        }

        [TestMethod]
        public void PeopleRepository_ShouldBeNullAndBadRequestForNullUrl()
        {
            peopleRepository = new PeopleRepository(null);
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNull(peopleRepository.People);
            Assert.IsTrue(peopleRepository.IsInitialized);
            Assert.AreEqual(HttpStatusCode.BadRequest, peopleRepository.HttpStatusCode);
        }

        [TestMethod]
        public void PeopleRepository_ShouldBeNullAndBadRequestForEmptyUrl()
        {
            peopleRepository = new PeopleRepository(string.Empty);
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNull(peopleRepository.People);
            Assert.IsTrue(peopleRepository.IsInitialized);
            Assert.AreEqual(HttpStatusCode.BadRequest, peopleRepository.HttpStatusCode);
        }

        [TestMethod]
        public void PeopleRepository_ShouldBeNullAndBadRequestForInvalidUrl()
        {
            peopleRepository = new PeopleRepository("1");
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNull(peopleRepository.People);
            Assert.IsTrue(peopleRepository.IsInitialized);
            Assert.AreEqual(HttpStatusCode.BadRequest, peopleRepository.HttpStatusCode);
        }

        [TestMethod]
        public void PeopleRepository_ShouldBeNullAndNotFoundForNotFoundUrl()
        {
            peopleRepository = new PeopleRepository("http://agl-developer-test.azurewebsites.net/people1.json");
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNull(peopleRepository.People);
            Assert.IsTrue(peopleRepository.IsInitialized);
            Assert.AreEqual(HttpStatusCode.NotFound, peopleRepository.HttpStatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PeopleRepository_ShouldBeNullAndNotFoundForInvalidDataSourceUrl()
        {
            peopleRepository = new PeopleRepository("http://agl-developer-test.azurewebsites.net/");
            Assert.IsFalse(peopleRepository.IsInitialized);
            Assert.IsNull(peopleRepository.People);
        }
    }
}
