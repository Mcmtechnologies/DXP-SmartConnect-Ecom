using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductBaseSearchDto
    {
        public int Count { set; get; }
        public int Total { set; get; }
        public IList<ProductDto> Items { get; set; } = new List<ProductDto>();
        public string RetailerCategoryId { set; get; }
    }
}
