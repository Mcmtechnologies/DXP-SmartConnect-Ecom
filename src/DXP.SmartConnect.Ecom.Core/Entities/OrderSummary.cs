using DXP.SmartConnect.Ecom.SharedKernel;
using System;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderSummary : BaseEntity<string>
    {
        public int ItemCount { set; get; }
        public string TaxTotal { set; get; }
        public string ServiceFee { set; get; }
        public int PointsEarned { set; get; }
        public string CustomerCredit { set; get; }
        public string Total { set; get; }
        public string OrderValue { set; get; }
    }
}