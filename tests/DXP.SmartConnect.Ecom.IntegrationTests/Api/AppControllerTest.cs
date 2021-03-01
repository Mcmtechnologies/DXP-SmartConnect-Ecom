using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DXP.SmartConnect.Ecom.IntegrationTests.Api
{
    public class AppControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AppControllerTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task InfoTest_ReturnsInfo()
        {
            // act
            var response = await _client.GetAsync("/readme");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Contains("server", stringResponse);
        }
    }
}
