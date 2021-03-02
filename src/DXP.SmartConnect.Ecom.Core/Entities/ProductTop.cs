using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ProductTop
    {
        public string Title { set; get; }
        public int Count { set; get; }
        public int Total { set; get; }
        public IDictionary<string, ProductBaseSearch> SubCategories { get; set; }
        public string RetailerCategoryId { set; get; }
        public int Take { set; get; }
    }
}
