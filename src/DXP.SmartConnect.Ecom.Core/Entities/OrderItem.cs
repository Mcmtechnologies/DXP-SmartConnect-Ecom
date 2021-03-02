using DXP.SmartConnect.Ecom.SharedKernel;
using System;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderItem : BaseEntity<string>
    {
        public string Name { set; get; }
        public string Sku { set; get; }
        public string Category { set; get; }
        public string Image { set; get; }
        public string Brand { set; get; }
        public string Quantity { set; get; }
        public string Note { set; get; }
        public string Price { set; get; }
        public string LineItemPrice { set; get; }
        public string TotalPrice { set; get; }
        public bool AllowSubstitution { set; get; }
    }
}