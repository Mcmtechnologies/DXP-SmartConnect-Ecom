using DXP.SmartConnect.Ecom.SharedKernel;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class Order : BaseEntity<string>
    {
        public OrderSummary Summary { set; get; }
        public string CustomerId { set; get; }
        public string OrderReference { set; get; }
        public string Status { set; get; }
        public string NotificationPhoneNumber { set; get; }
        public object ModifyStatus { set; get; }
        public OrderFulfilment Fulfilment { set; get; }
        public Store Store { set; get; }
        public object PaymentMethods { set; get; }
        public IList<OrderItem> LineItems { set; get; }
        public object LineItemFulfilmentResults { set; get; }
        public object VoucherCodes { set; get; }
        public string Notes { set; get; }
        public int ItemCount { set; get; }
        public object Promotions { set; get; }
        public object Attributes { set; get; }
        public bool CanModify { set; get; }
    }
}