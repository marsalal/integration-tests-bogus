using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using integration_tests.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Person = integration_tests.Data.Models.Person;

namespace sample.tests
{
    public class PersonControllerTest : IClassFixture<BaseWebApplicationFactory<integration_tests.Startup>>
    {
        private readonly BaseWebApplicationFactory<integration_tests.Startup> _factory;
        public PersonControllerTest(BaseWebApplicationFactory<integration_tests.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_Returns_ApplicationJsonHeaders_Test()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/person");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task GetById_NotFound_Test()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"/api/person/{Guid.NewGuid()}");


            Assert.Equal(StatusCodes.Status404NotFound, (int)response.StatusCode);
        }

        [Fact]
        public async Task GetById_Returns_Person_Ok_Test()
        {
            var client = _factory.CreateClient();
            var fakePersons = HelperTests.GetFakePersons();
            var randomPerson = fakePersons.FirstOrDefault();
            var response = await client.GetAsync($"/api/person/{randomPerson.Id}");


            response.EnsureSuccessStatusCode();
            var personReturned = JsonSerializer.Deserialize<Person>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            Assert.Equal(fakePersons.First(x => x.Id == personReturned.Id).Email, personReturned.Email);
        }
    }
}
