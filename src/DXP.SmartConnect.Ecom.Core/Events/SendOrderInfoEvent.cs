using DXP.SmartConnect.Ecom.SharedKernel;
using MediatR;

namespace DXP.SmartConnect.Ecom.Core.Events
{
    public class SendOrderInfoEvent : BaseDomainEvent, INotification
    {
        public string AccessToken { get; set; }
        public string SQSQueueUrl { get; set; }
        public string OrderReferenceId { get; set; }
        public string SessionsId { get; set; }
    }
}
