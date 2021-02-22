using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace sample.tests
{
    public class PersonControllerTest : IClassFixture<WebApplicationFactory<integration_tests.Startup>>
    {
        private readonly WebApplicationFactory<integration_tests.Startup> _factory;
        public PersonControllerTest(WebApplicationFactory<integration_tests.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_Returns_ApplicationJsonHeaders_Test()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/api/person");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
