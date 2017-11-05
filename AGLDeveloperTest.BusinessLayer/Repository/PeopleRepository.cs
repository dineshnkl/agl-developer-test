using System;
using System.Collections.Generic;
using System.Net;
using AGLDeveloperTest.BusinessLayer.Entity;
using AGLDeveloperTest.BusinessLayer.Helper;

namespace AGLDeveloperTest.BusinessLayer.Repository
{
    public class PeopleRepository : IPeopleRepository
    {
        private Lazy<List<Person>> people;
        private HttpStatusCode httpStatusCode;

        public IEnumerable<Person> People { get { return people.Value; } }
        public HttpStatusCode HttpStatusCode { get { return httpStatusCode; } }
        public bool IsInitialized { get; private set; }

        public const string PeopleDataUrl = "http://agl-developer-test.azurewebsites.net/people.json";

        public PeopleRepository()
        {
            people = new Lazy<List<Person>>(() => GetPeople());
        }

        private List<Person> GetPeople()
        {
            List<Person> peopleList = null;
            using (var httpResponseMessage = WebDownloadHelper.Get(PeopleDataUrl))
            {
                httpStatusCode = httpResponseMessage.StatusCode;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseText = WebDownloadHelper.ReadString(httpResponseMessage).Result;
                    peopleList = JsonHelper.Deserialize<List<Person>>(responseText);
                }
            }
            IsInitialized = true;
            return peopleList;
        }
    }
}
