using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ShoppingHistoryPaging
    {
        public ShoppingHistoryPaging()
        {
            Items = new List<ShoppingHistoryItem>();
        }
        public List<ShoppingHistoryItem> Items { set; get; }
        public int Total { set; get; }
        public int Count { set; get; }
    }
}
