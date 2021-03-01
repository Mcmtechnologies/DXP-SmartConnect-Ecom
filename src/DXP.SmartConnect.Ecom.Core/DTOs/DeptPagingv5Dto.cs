using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class DeptPagingv5Dto
    {
        
        public IList<DeptV5Dto> Depts { get; set; }

        
        public IList<DeptSummaryDto> DeptSummary { get; set; }

        
        public IList<AttrSummaryDto> AttributeSummary { get; set; }

        
        public IList<string> BrandSummary { get; set; }
    }
}
