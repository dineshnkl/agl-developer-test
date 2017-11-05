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
    }
}
