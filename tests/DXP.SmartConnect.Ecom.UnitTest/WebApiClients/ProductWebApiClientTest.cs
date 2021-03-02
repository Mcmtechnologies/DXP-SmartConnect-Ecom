using DXP.SmartConnect.Ecom.Core.Entities;
using DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DXP.SmartConnect.Ecom.UnitTests.WebApiClients
{
    public class ProductWebApiClientTest
    {
        private readonly ProductWebApiClient _productWebApiClient;

        public ProductWebApiClientTest()
        {
            // Setup logger
            var mockLogger = new Mock<ILogger<ProductWebApiClient>>();

            // Setup httpclient
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://storefrontgateway.unt.stg.v8.commerce.mi9cloud.com/api/")
            };

            _productWebApiClient = new ProductWebApiClient(mockLogger.Object, httpClient);
        }

        [Fact]
        public async Task GetProductByUpcAsyncTest_ReturnProduct()
        {
            // arrange 
            var storeId = 502;
            var upc = "00055991071034";

            // act
            var result = await _productWebApiClient.GetProductByUpcAsync(storeId, upc);

            // assert
            Assert.IsType<Product>(result);
        }
    }
}
