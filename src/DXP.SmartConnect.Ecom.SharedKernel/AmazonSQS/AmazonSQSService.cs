using Amazon.SQS;
using Amazon.SQS.Model;
using DXP.SmartConnect.Ecom.SharedKernel.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.SharedKernel.AmazonSQS
{
    public class AmazonSQSService : IAmazonSQSService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger<AmazonSQSService> _logger;

        public AmazonSQSService(IAmazonSQS sqsClient, ILogger<AmazonSQSService> logger)
        {
            _sqsClient = sqsClient;
            _logger = logger;
        }

        public async Task<CreateQueueResponse> CreateQueueAsync(string queueName, bool isFifo, string deadLetterQueueArn = null, string maxReceiveCount = null)
        {
            var attributes = new Dictionary<string, string>();
            // Add attribute for fifo queue
            if (isFifo)
            {
                attributes.Add(QueueAttributeName.FifoQueue, isFifo.ToString());
                queueName += ".fifo";
            }
            // If a dead-letter queue is given, create a message queue
            if (!string.IsNullOrEmpty(deadLetterQueueArn))
            {
                attributes.Add(QueueAttributeName.RedrivePolicy,
                    $"{{\"deadLetterTargetArn\":\"{deadLetterQueueArn}\"," +
                    $"\"maxReceiveCount\":\"{maxReceiveCount}\"}}");
            }

            CreateQueueResponse responseCreate = await _sqsClient.CreateQueueAsync(
                new CreateQueueRequest
                {
                    QueueName = queueName,
                    Attributes = attributes
                });

            return responseCreate;
        }

        public async Task<bool> DeleteMessageBatchAsync(string queueUrl, Dictionary<string, string> messagesDict)
        {
            var messages = new List<DeleteMessageBatchRequestEntry>();
            foreach (var item in messagesDict)
            {
                messages.Add(new DeleteMessageBatchRequestEntry
                {
                    Id = item.Key,
                    ReceiptHandle = item.Value
                });
            }

            var response = await _sqsClient.DeleteMessageBatchAsync(queueUrl, messages);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl)
        {
            return await _sqsClient.DeleteQueueAsync(queueUrl);
        }

        public async Task<ReceiveMessageResponse> ReceiveMessageAsync(string queueUrl, int maxMessages = 1, int waitTime = 0)
        {
            return await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = queueUrl,
                MaxNumberOfMessages = maxMessages,
                WaitTimeSeconds = waitTime
            });
        }

        public async Task<bool> SendMessageAsync(string queueUrl, string message)
        {
            try
            {
                var response = await _sqsClient.SendMessageAsync(queueUrl, message);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"{nameof(SendMessageAsync)} with:{Environment.NewLine} " +
                    $"QueueUrl: {queueUrl} and Message: {message}" +
                    $"throw exception: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendMessageBatchAsync(string queueUrl, Dictionary<string, string> messagesDict)
        {
            try
            {
                var messages = new List<SendMessageBatchRequestEntry>();
                foreach (var item in messagesDict)
                {
                    messages.Add(new SendMessageBatchRequestEntry
                    {
                        Id = item.Key,
                        MessageBody = item.Value
                    });
                }

                var response = await _sqsClient.SendMessageBatchAsync(queueUrl, messages);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                var dictionaryString = new StringBuilder(string.Empty);
                foreach (var item in messagesDict)
                {
                    dictionaryString.Append(item);
                }

                _logger.LogError(
                     $"{nameof(SendMessageBatchAsync)} with:{Environment.NewLine} " +
                     $"QueueUrl: {queueUrl} and Message: {dictionaryString}" +
                     $"throw exception: {ex.Message}");
                return false;
            }
        }

        public async Task<ListQueuesResponse> ShowQueuesAsync()
        {
            return await _sqsClient.ListQueuesAsync("");
        }
    }
}
