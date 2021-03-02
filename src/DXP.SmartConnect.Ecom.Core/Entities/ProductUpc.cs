using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ProductUpc : Product
    {
        public new string DefaultCategory { set; get; }
        public Dictionary<string, string> PrimaryImage { set; get; }
    }
}
