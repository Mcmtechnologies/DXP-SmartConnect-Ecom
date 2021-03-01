using DXP.SmartConnect.Ecom.Core.DTOs;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DXP.SmartConnect.Ecom.IntegrationTests.Api
{
    public class ProductControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public ProductControllerTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProductByUpcTest_ReturnsProduct()
        {
            // arrange 
            var storeId = "502";
            var upc = "00055991071034";

            // act
            var response = await _client.GetAsync($"/api/Product/GetProductByUpc?storeId={storeId}&upc={upc}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDto>(stringResponse);

            // assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task GetProductByUpcTest_ReturnsProductNotFound()
        {
            // arrange 
            var storeId = "5022";
            var upc = "00055991071032";

            // act
            var response = await _client.GetAsync($"/api/Product/GetProductByUpc?storeId={storeId}&upc={upc}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Contains("ApiException", stringResponse);
        }

        [Fact]
        public async Task GetProductByUpcDbTest_ReturnsProduct()
        {
            // arrange 
            var storeId = "502";
            var upc = "38375";

            // act
            var response = await _client.GetAsync($"/api/Product/GetProductByUpcDb?storeId={storeId}&upc={upc}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDto>(stringResponse);

            // assert
            Assert.NotNull(product);
        }

        [Fact]
        public async Task GetProductByUpcDbTest_ReturnsProductNoContent()
        {
            // arrange 
            var storeId = "5022";
            var upc = "000559";

            // act
            var response = await _client.GetAsync($"/api/Product/GetProductByUpcDb?storeId={storeId}&upc={upc}");
            var stringResponse = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.NotNull(stringResponse);
        }
    }
}
