using DXP.SmartConnect.Ecom.SharedKernel;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class CustomerAddress : BaseEntity<string>
    {
        public string Country { set; get; }
        public string Line1 { set; get; }
        public string Line2 { set; get; }
        public string PostCode { set; get; }
        public string Region { set; get; }
        public string Source { set; get; }
        public string TownCity { set; get; }
        public string Phone { set; get; }
    }
}
