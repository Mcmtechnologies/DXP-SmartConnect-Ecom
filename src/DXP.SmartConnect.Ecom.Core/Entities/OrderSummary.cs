using DXP.SmartConnect.Ecom.SharedKernel;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderSummary : BaseEntity<string>
    {
        public int ItemCount { set; get; }
        public string TaxTotal { set; get; }
        public string ServiceFee { set; get; }
        public decimal PointsEarned { set; get; }
        public string CustomerCredit { set; get; }
        public string Total { set; get; }
        public string OrderValue { set; get; }
    }
}