using System.Collections.Generic;

namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class ShoppingHistoryItem
    {
        public ShoppingHistoryItem()
        {
            PointsBasedPromotions = new List<Promotion>();
            PromotionInfo = new List<Promotion>();
        }

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
        public string SellBy { set; get; }
        public Dictionary<string, object> Attributes { set; get; }
        public ProductCategory DefaultCategory { set; get; }
        public ProductCategory Categories { set; get; }
        public bool IsFavorite { set; get; }
        public bool IsPastPurchased { set; get; }
        public Dictionary<string, string> Image { set; get; }
        public List<Promotion> PromotionInfo { set; get; }
        public List<Promotion> PointsBasedPromotions { set; get; }
        public Weight WeightIncrement { set; get; }
        public RuleMessage ShoppingRuleMessage { set; get; }
        public bool Available { set; get; }
    }
}
