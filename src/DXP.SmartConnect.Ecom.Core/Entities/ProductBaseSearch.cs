using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ProductBaseSearch
    {
        public int Count { set; get; }
        public int Total { set; get; }
        public IList<Product> Items { get; set; } = new List<Product>();
        public string RetailerCategoryId { set; get; }
    }
}
