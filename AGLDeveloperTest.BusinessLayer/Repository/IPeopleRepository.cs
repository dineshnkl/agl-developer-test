using System.Collections.Generic;
using AGLDeveloperTest.BusinessLayer.Entity;
using System.Net;

namespace AGLDeveloperTest.BusinessLayer.Repository
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> People { get; }
        HttpStatusCode HttpStatusCode { get; }
        bool IsInitialized { get; }
    }
}
