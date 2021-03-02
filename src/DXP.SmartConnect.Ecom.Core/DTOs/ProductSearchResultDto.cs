using System;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductSearchResultDto //ProductSearchRO
    {
        public string Id { get; set; }
        public String Name { get; set; }
        public String UPC { get; set; }
        public string DefaultImage { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<short> Status { get; set; }
        public string CompanyId { get; set; }
        public AclDto Permissions { get; set; }
        public string ValueType { get; set; }
        public virtual IList<ProductVariantDto> ProductVariants { get; set; }
        public virtual bool IsFavorited { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string PriceText { get; set; }
        public string SaleInfo { get; set; }
        public string Category { get; set; }
        public bool OnSale { get; set; }
        public double Price { get; set; }
        public int Quantity { set; get; }
        public string Note { set; get; }
    }
}
