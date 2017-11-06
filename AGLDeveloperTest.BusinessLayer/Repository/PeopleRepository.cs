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
        public string DataSourceUrl { get; private set; }

        private const string DefaultDataSourceUrl = "http://agl-developer-test.azurewebsites.net/people.json";

        public PeopleRepository()
        {
            people = new Lazy<List<Person>>(() => GetPeople());
            DataSourceUrl = DefaultDataSourceUrl;
        }

        public PeopleRepository(string dataSourceUrl)
        {
            people = new Lazy<List<Person>>(() => GetPeople());
            DataSourceUrl = dataSourceUrl;
        }

        private List<Person> GetPeople()
        {
            List<Person> peopleList = null;
            using (var httpResponseMessage = WebDownloadHelper.Get(DataSourceUrl))
            {
                httpStatusCode = httpResponseMessage.StatusCode;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var responseText = WebDownloadHelper.ReadString(httpResponseMessage).Result;
                    try
                    {
                        peopleList = JsonHelper.Deserialize<List<Person>>(responseText);
                    }
                    catch (ArgumentException)
                    {
                        IsInitialized = true;
                        throw;
                    }
                }
            }
            IsInitialized = true;
            return peopleList;
        }
    }
}
