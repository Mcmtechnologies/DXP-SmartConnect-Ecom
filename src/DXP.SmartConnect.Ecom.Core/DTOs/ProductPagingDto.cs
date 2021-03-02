using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductPagingDto
    {
        public ProductPagingDto()
        {
            Products = new List<ProductDto>();
            DeptSummary = new List<DeptSummaryDto>();
            AttributeSummary = new List<AttributeSummaryDto>();
        }

        public IList<ProductDto> Products { set; get; }
        public IList<DeptSummaryDto> DeptSummary { get; set; }
        public IList<AttributeSummaryDto> AttributeSummary { get; set; }
        public IList<string> BrandSummary { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
