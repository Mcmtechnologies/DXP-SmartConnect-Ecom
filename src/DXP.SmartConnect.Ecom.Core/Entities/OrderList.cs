using DXP.SmartConnect.Ecom.SharedKernel;
using System;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderList : BaseEntity<string>
    {
        public string OrderRef { set; get; }
        public string FulfilmentType { set; get; }
        public string FulfilmentDateTime { set; get; }
        public string FulfilmentReservationReference { set; get; }
        public string RetailerStoreId { set; get; }
        public string DatePlaced { set; get; }
        public CustomerAddress FulfilmentLocation { set; get; }
        public string EstTotal { set; get; }
        public string Status { set; get; }
        public IList<OrderListItem> LineItems { set; get; }
        public bool CanModify { set; get; }
        public IList<string> AllowedStateTransitions { set; get; }
    }
}