using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.SharedKernel.Interfaces
{
    public interface IAmazonSQSService
    {
        /// <summary>
        /// Show a list of the existing queues.
        /// </summary>
        /// <returns>List queues.</returns>
        Task<ListQueuesResponse> ShowQueuesAsync();
        /// <summary>
        /// Create the queue.
        /// </summary>
        /// <param name="queueName">Queue name</param>
        /// <param name="isFifo">Specify the FifoQueue or standard </param>
        /// <param name="deadLetterQueueArn">Dead-letter queue ARN</param>
        /// <param name="maxReceiveCount">Maximum message receives</param>
        /// <returns>The Queue.</returns>
        /// <remarks>If you provide the name of an existing queue along with the exact names and values of all the queue's attributes, CreateQueue returns the queue URL for the existing queue.
        /// After you create a queue, you must wait at least one second after the queue is created to be able to use the queue.</remarks>
        Task<CreateQueueResponse> CreateQueueAsync(string queueName, bool isFifo, string deadLetterQueueArn = null, string maxReceiveCount = null);
        /// <summary>
        /// Delete an SQS queue.
        /// </summary>
        /// <param name="queueUrl">Queue url</param>
        /// <returns>The status.</returns>
        /// <remarks>If you delete a queue, you must wait at least 60 seconds before creating a queue with the same name.</remarks>
        Task<DeleteQueueResponse> DeleteQueueAsync(string queueUrl);
        /// <summary>
        /// Put a message on a queue (standard queue).
        /// </summary>
        /// <param name="queueUrl">Queue url</param>
        /// <param name="message">Message body</param>
        /// <returns>Message Response.</returns>
        Task<bool> SendMessageAsync(string queueUrl, string message);
        /// <summary>
        /// Put a batch of messages on a queue (standard queue).
        /// </summary>
        /// <param name="queueUrl">Queue url</param>
        /// <param name="messagesDict">Dictionary of messages (id, message)</param>
        /// <returns>Message Batch Response.</returns>
        Task<bool> SendMessageBatchAsync(string queueUrl, Dictionary<string, string> messagesDict);
        /// <summary>
        /// Get a message from the given queue.
        /// </summary>
        /// <param name="queueUrl">Queue url</param>
        /// <param name="maxMessages">Maximum messages to get</param>
        /// <param name="waitTime">Waiting duration</param>
        /// <returns>List Messages Response.</returns>
        Task<ReceiveMessageResponse> ReceiveMessageAsync(string queueUrl, int maxMessages = 1, int waitTime = 0);
        /// <summary>
        /// Delete a batch of messages on a queue.
        /// </summary>
        /// <param name="queueUrl">Queue url</param>
        /// <param name="messagesDict">Dictionary of messages (id, receiptHandle)</param>
        /// <returns>Delete Message Batch Response.</returns>
        Task<bool> DeleteMessageBatchAsync(string queueUrl, Dictionary<string, string> messagesDict);
    }
}
