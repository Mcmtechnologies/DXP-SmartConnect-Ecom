using System.Collections.Generic;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductUpcPagingDto
    {
        public ProductUpcPageDto page { get; set; }
        public IList<ProductDetailDto> responseList { get; set; }
        public IList<MhSummaryDto> mhSummary { get; set; } = new List<MhSummaryDto>();
        public IList<DeptSummaryDto> mwgSummary { get; set; }
        public IList<AttributeSummaryDto> attributeSummary { get; set; }
        public IList<string> brandSummary { get; set; }
    }
}
