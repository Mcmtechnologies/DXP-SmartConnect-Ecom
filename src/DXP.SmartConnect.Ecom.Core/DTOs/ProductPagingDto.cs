using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductPagingDto
    {
        public IList<ProductDto> Products { set; get; } = new List<ProductDto>();
        public IList<DeptSummaryDto> DeptSummary { get; set; } = new List<DeptSummaryDto>();
        public IList<AttrSummaryDto> AttributeSummary { get; set; } = new List<AttrSummaryDto>();
        public IList<string> BrandSummary { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
