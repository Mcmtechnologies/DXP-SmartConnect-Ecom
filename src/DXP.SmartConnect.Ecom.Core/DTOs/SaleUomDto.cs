using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class SaleUomDto
    {
        public Nullable<int> qtyUomId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }
}
