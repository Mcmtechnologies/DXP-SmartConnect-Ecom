using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class Promotion
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string AdditionalInformation { set; get; }
        public DateTime? StartDate { set; get; }
        public DateTime? EndDate { set; get; }
        public int? Limit { set; get; }
        public int? Threshold { set; get; }
        public int? MinimumQuantity { set; get; }
        public bool PointsBased { set; get; }
        public string ImageUrl { set; get; }
    }
}
