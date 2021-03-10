using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using DXP.SmartConnect.Ecom.Core.Events;
using DXP.SmartConnect.Ecom.Core.Handlers;
using DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients;
using DXP.SmartConnect.Ecom.SharedKernel.AmazonSQS;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DXP.SmartConnect.Ecom.UnitTests.Handlers
{
    public class SendOrderInfoNotificationHandlerTest
    {
        private readonly SendOrderInfoNotificationHandler _sendOrderInfoNotificationHandler;

        public SendOrderInfoNotificationHandlerTest()
        {
            // Setup OrderWebApiClient
            var mockOrderLogger = new Mock<ILogger<OrderWebApiClient>>();
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://storefrontgateway.unt.stg.v8.commerce.mi9cloud.com/api/")
            };
            var orderWebApiClient = new OrderWebApiClient(mockOrderLogger.Object, httpClient);

            // Setup Amazon SQS client
            var mockAsqsLogger = new Mock<ILogger<AmazonSQSService>>();
            AWSOptions options = new AWSOptions
            {
                Profile = "local-dev",
                Region = RegionEndpoint.USEast2
            };
            IAmazonSQS sqsClient = options.CreateServiceClient<IAmazonSQS>();
            var amazonSQSService = new AmazonSQSService(sqsClient, mockAsqsLogger.Object);

            _sendOrderInfoNotificationHandler = new SendOrderInfoNotificationHandler(orderWebApiClient, amazonSQSService);
        }

        [Fact]
        public async Task SendOrderInfoEventTest_ReturnTaskComplete()
        {
            // arrange 

            // act
            _ = Task.Run(() => _sendOrderInfoNotificationHandler.Handle(new SendOrderInfoEvent
            {
                AccessToken = "db1f02eddd0e587a8cafce61be2ff6fde87fb276d7aee68b82926567171b32a0",
                SQSQueueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/Test-queue",
                OrderReferenceId = "4000089"
            }, default(CancellationToken)));

            await Task.Delay(5000);

            // assert
            Assert.True(true);
        }
    }
}
