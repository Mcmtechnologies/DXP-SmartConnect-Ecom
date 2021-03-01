using System;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductVariantDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDownload { get; set; }
        public int? MaxNumberOfDownloads { get; set; }
        public DateTime? DownloadExpiration { get; set; }
        public int? StockQuantity { get; set; }
        public int? StockAvailableQuantity { get; set; }
        public double? Price { get; set; }
        public double? SalePrice { get; set; }
        public decimal? ProductCost { get; set; }
        public double? Weight { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public DateTime? AvailableStartDate { get; set; }
        public DateTime? AvailableEndDate { get; set; }
        public bool? Customizable { get; set; }
        public string Tax { get; set; }
        public bool? IsShipEnabled { get; set; }
        public string Sku { get; set; }
        public int? GroupId { get; set; }
        public int? DisplayOrder { get; set; }
        public string Vendor { get; set; }
        public int BinId { get; set; }
        public int? StoreId { get; set; }
        public int? AverageShipDays { get; set; }
        public int? CurrentInventory { get; set; }
        public int? LeadTime { get; set; }
        public int? ReplenishmentThreshold { get; set; }
        public string ProductVariantType { get; set; }
        public string Upc { get; set; }
        public bool? OnSale { get; set; }
        public Nullable<decimal> RetailPrice { get; set; }
        public bool? IsFreeShipping { get; set; }
    }
}
