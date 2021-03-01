using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class AttributeSummaryDto
    {
        public Nullable<int> FieldId { get; set; }
        public string FieldLabel { get; set; }
        public Nullable<int> FieldTypeId { get; set; }
        public string FieldValue { get; set; }
        public string GroupLabel { get; set; }
        public string TabLabel { get; set; }
        public string ViewToolTip { get; set; }
        public string EditToolTip { get; set; }
        public string Status { get; set; }
        public string DynamicLabel { get; set; }
    }
}
