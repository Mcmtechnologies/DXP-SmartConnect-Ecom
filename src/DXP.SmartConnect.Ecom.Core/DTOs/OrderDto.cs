using DXP.SmartConnect.Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderDto
    {
        public IList<OrderInfoDto> Orders { get; set; } = new List<OrderInfoDto>();
    }
}
