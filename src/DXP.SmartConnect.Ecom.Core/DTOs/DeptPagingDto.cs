using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class DeptPagingDto
    {

        public IList<DeptDto> Depts { get; set; }


        public IList<DeptSummaryDto> DeptSummary { get; set; }


        public IList<AttrSummaryDto> AttributeSummary { get; set; }


        public IList<string> BrandSummary { get; set; }
    }
}
