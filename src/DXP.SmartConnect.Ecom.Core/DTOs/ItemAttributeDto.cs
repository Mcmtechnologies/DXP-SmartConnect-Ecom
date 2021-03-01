using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ItemAttributesDto
    {
        public IdItemAttributesDto id { get; set; }
        public string fieldValue { get; set; }
    }
    public class IdItemAttributesDto
    {
        public Nullable<int> itemId { get; set; }
        public CorCustomFieldItemAttributesDto corCustomField { get; set; }
    }
    public class CorCustomFieldItemAttributesDto
    {
        public Nullable<int> fieldId { get; set; }
        public string fieldLabel { get; set; }
        public Nullable<int> fieldTypeId { get; set; }
        public string groupLabel { get; set; }
        public string tabLabel { get; set; }
        public string viewTooltip { get; set; }
        public string editTooltip { get; set; }
        public string status { get; set; }
        public string dynamicLabel { get; set; }
    }
}
