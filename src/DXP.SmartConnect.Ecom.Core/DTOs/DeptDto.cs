using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class DeptDto
    {
        public string DeptName { get; set; }
        public string DeptId { get; set; }
        public IList<ProductDto> Products { set; get; }
    }
}
