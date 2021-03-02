using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Amazon.SQS.Model;
using DXP.SmartConnect.Ecom.SharedKernel.AmazonSQS;
using DXP.SmartConnect.Ecom.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DXP.SmartConnect.Ecom.UnitTests.SharedKernel
{
    public class AmazonSQSServiceTest
    {
        private readonly IAmazonSQSService _amazonSQSService;

        public AmazonSQSServiceTest()
        {
            var mockLogger = new Mock<ILogger<AmazonSQSService>>();

            AWSOptions options = new AWSOptions
            {
                Profile = "local-dev",
                Region = RegionEndpoint.USEast2
            };
            IAmazonSQS sqsClient = options.CreateServiceClient<IAmazonSQS>();

            _amazonSQSService = new AmazonSQSService(sqsClient, mockLogger.Object);
        }

        [Fact]
        public void ShowQueues_ReturnListQueues()
        {
            // act
            var result = _amazonSQSService.ShowQueuesAsync().GetAwaiter().GetResult();
            // assert
            Assert.IsType<ListQueuesResponse>(result);
        }

        [Fact]
        public void CreateQueue_ReturnQueue()
        {
            var queueName = "Test-queue";
            var isFifo = false;
            // act
            var result = _amazonSQSService.CreateQueueAsync(queueName, isFifo).GetAwaiter().GetResult();
            // assert
            Assert.IsType<CreateQueueResponse>(result);
        }

        [Fact]
        public void DeleteQueue_ReturnOK()
        {
            var queueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/queueName";
            // act
            var result = _amazonSQSService.DeleteQueueAsync(queueUrl).GetAwaiter().GetResult();
            // assert
            Assert.IsType<DeleteQueueResponse>(result);
        }

        [Fact]
        public void SendMessage_ReturnOK()
        {
            var queueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/Test-queue";
            var message = "Test send from api";
            // act
            var result = _amazonSQSService.SendMessageAsync(queueUrl, message).GetAwaiter().GetResult();
            // assert
            Assert.True(result);
        }

        [Fact]
        public void SendMessageBatch_ReturnOK()
        {
            var queueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/Test-queue";
            var messageDict = new Dictionary<string, string>();
            messageDict.Add("1", "Message id 7");
            messageDict.Add("2", "Message id 8");
            // act
            var result = _amazonSQSService.SendMessageBatchAsync(queueUrl, messageDict).GetAwaiter().GetResult();
            // assert
            Assert.True(result);
        }

        [Fact]
        public void DeleteMessageBatch_ReturnOK()
        {
            var queueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/Test-queue";
            var messageDict = new Dictionary<string, string>();
            messageDict.Add("0472bda7-023c-43c2-9b34-c2b0bb41293c", "");
            // act
            var result = _amazonSQSService.DeleteMessageBatchAsync(queueUrl, messageDict).GetAwaiter().GetResult();
            // assert
            Assert.True(result);
        }

        [Fact]
        public void ReceiveMessage_ReturnListMessages()
        {
            var queueUrl = "https://sqs.us-east-2.amazonaws.com/010873991698/Test-queue";
            var maxMessage = 10;
            var waitTime = 10;
            // act
            var result = _amazonSQSService.ReceiveMessageAsync(queueUrl, maxMessage, waitTime).GetAwaiter().GetResult();
            // assert
            Assert.IsType<ReceiveMessageResponse>(result);
        }
    }
}
