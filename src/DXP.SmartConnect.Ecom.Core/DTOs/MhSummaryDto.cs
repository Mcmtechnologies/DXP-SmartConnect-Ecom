using System.Collections.Generic;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class MhSummaryDto
    {
        public string departmentId { get; set; }
        public string department { get; set; }
        public IList<SubDept1sDto> subDept1s { get; set; } = new List<SubDept1sDto>();
        public IList<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
