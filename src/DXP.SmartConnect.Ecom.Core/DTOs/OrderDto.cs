using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderDto
    {
        public IList<OrderInfoDto> Orders { get; set; } = new List<OrderInfoDto>();
    }
}
