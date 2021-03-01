using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductUpcPageDto
    {
            public IList<ProductDetailDto> content { get; set; }
            public int totalPages { get; set; }
            public int totalElements { get; set; }
            public int pageSize { get; set; }
            public int pageNumber { get; set; }
    }
}
