using DXP.SmartConnect.Ecom.SharedKernel;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderPage : BaseEntity<string>
    {
        public int Total { set; get; }
        public int Count { set; get; }
        public IList<OrderList> Items { set; get; }
    }
}