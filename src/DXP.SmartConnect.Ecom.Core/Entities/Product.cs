using DXP.SmartConnect.Ecom.SharedKernel;
using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class Product : BaseEntity<string>
    {
        public string ProductId { set; get; }
        public string Sku { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Brand { set; get; }
        public string PriceLabel { set; get; }
        public string Price { set; get; }
        public string WasPrice { set; get; }
        public string PricePerUnit { set; get; }
        public ProductUnit UnitOfSize { set; get; }
        public ProductUnit UnitOfMeasure { set; get; }
        public ProductUnit UnitOfPrice { set; get; }
        public string Sellby { set; get; }
        public IDictionary<string, object> Attributes { set; get; }
        public IList<ProductCategory> DefaultCategory { set; get; }
        public IList<ProductCategory> Categories { set; get; }
        public bool IsFavorite { set; get; }
        public bool IsPastPurchased { set; get; }
        public IDictionary<string, string> Image { set; get; }
        public bool Available { set; get; }
        public object NutritionProfiles { set; get; }
    }
}