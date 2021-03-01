using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ProductAttribute
    {
        public string V8InternalProductId { set; get; }
        [DisplayName("Item Type")]
        public string ItemType { set; get; }
        [DisplayName("Item Group Number")]
        public string ItemGroupNumber { set; get; }
        [DisplayName("Stock Unit")]
        public string StockUnit { set; get; }
        [DisplayName("Till Code Type")]
        public string TillCodeType { set; get; }
        [DisplayName("Age Restriction")]
        public string AgeRestriction { set; get; }
        [DisplayName("Guest Item Name")]
        public string GuestItemName { set; get; }
        public string Ecommerce { set; get; }
        [DisplayName("Total On-Hand Qty")]
        public int TotalOnHandQty { set; get; }
        [DisplayName("Consumer Shelf Life")]
        public int ConsumerShelfLife { set; get; }
    }
}
