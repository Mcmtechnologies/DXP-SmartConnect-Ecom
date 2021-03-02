using DXP.SmartConnect.Ecom.SharedKernel;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderListItem : BaseEntity<string>
    {
        public string Sku { set; get; }
        public IDictionary<string, string> Image { set; get; }
        public string Quantity { set; get; }
        public string AllowSubstitution { set; get; }
        public string Note { set; get; }
    }
}