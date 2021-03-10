using DXP.SmartConnect.Ecom.Core.Events;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.SharedKernel.Interfaces;
using MediatR;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Handlers
{
    public class SendOrderInfoNotificationHandler : INotificationHandler<SendOrderInfoEvent>
    {
        private readonly IOrderWebApiClient _orderWebApiClient;
        private readonly IAmazonSQSService _amazonSQSService;

        public SendOrderInfoNotificationHandler(
            IOrderWebApiClient orderWebApiClient,
            IAmazonSQSService amazonSQSService)
        {
            _orderWebApiClient = orderWebApiClient;
            _amazonSQSService = amazonSQSService;
        }

        /// <summary>
        /// Send order info and session id to Amazon SQS.
        /// </summary>
        /// <param name="notification">Event object </param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task Handle(SendOrderInfoEvent notification, CancellationToken cancellationToken)
        {
            var order = await _orderWebApiClient.GetOrderByReference(notification.AccessToken, notification.OrderReferenceId);
            if (order != null)
            {
                var queueUrl = notification.SQSQueueUrl;
                var messageBody = new
                {
                    Order = order
                };

                await _amazonSQSService.SendMessageAsync(queueUrl, JsonConvert.SerializeObject(messageBody));
            }
        }
    }
}
