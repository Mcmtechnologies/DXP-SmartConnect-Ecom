using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderInstoreUpcDto
    {
        public IList<string> UpcList { get; set; } = new List<string>();
    }
}