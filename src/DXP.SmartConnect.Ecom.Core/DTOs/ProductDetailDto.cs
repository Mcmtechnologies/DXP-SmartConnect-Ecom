using System;
using System.Collections.Generic;
using System.Text;
namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductDetailDto
    {
        public string keyId { get; set; }
        public string itemId { get; set; }
        public string itemNum { get; set; }
        public string nameForSales { get; set; }
        public string nameForPurchases { get; set; }
        public string gfItemDescription { get; set; }
        public string brandName { get; set; }
        public string barcodeNum { get; set; }
        public string barcodeTypeId { get; set; }
        public string imageUrl { get; set; }
        public string imageUrlSource { get; set; }
        public string upc { get; set; }
        public string mwgProductGuid { get; set; }
        public SaleUomDto salesUom { get; set; }
        public string description { get; set; }
        public Nullable<int> classId { get; set; }
        public Nullable<int> classXref { get; set; }
        public string departmentId { get; set; }
        public string department { get; set; }
        public string subDept1Id { get; set; }
        public string subDept1Parent { get; set; }
        public string subDepartment1 { get; set; }
        public string subDept2Id { get; set; }
        public string subDept2Parent { get; set; }
        public string subDepartment2 { get; set; }
        public StorageAreaDto storageArea { get; set; }
        public string status { get; set; }
        public IssueUomDto issueUom { get; set; }
        public ItemClassDto itemClass { get; set; }
        public string allowRetail { get; set; }
        public string primaryBarcode { get; set; }
        public Nullable<int> customerId { get; set; }
        public string regType { get; set; }
        public string regDesc { get; set; }
        public string regPriceMulti { get; set; }
        public double regPrice { get; set; }
        public string tprType { get; set; }
        public string tprDesc { get; set; }
        public string tprPriceMulti { get; set; }
        public string fulPriceMulti { get; set; }
        public double promoInd { get; set; }
        public DateTime priceDate { get; set; }
        public DateTime loadDate { get; set; }
        public DateTime regStart { get; set; }
        public DateTime regEnd { get; set; }
        public Nullable<int> movement { get; set; }
        public IList<ItemAttributesDto> itemAttributes { get; set; }
        public string unitSize { get; set; }
        /// <summary>
        /// ///////////////////// Not use
        /// </summary>
        public String ShortDescription { get; set; }
        public string CompanyId { get; set; }
        public string categoryGroupId { get; set; }
        public string categoryGroupName { get; set; }
        public double? tprPrice { get; set; }
        public bool? ShowSalePrice { get; set; }
        public string TaxClass { get; set; }
        public bool AvailableInAllStore { get; set; }
        public bool AvailableInStore { get; set; }
        public string DefaultImage { get; set; }
        public string WarrantyInfo { get; set; }
        public string Tags { get; set; }
        public string Condition { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<int> MinPurchaseQuantity { get; set; }
        public Nullable<int> MaxPurchaseQuantity { get; set; }
        public string ValueType { get; set; }
        public bool OnSale { get; set; }
        public string InventoryTracking { get; set; }
        public Nullable<int> CurrentStock { get; set; }
        public Nullable<int> LowStock { get; set; }
        public string Information { get; set; }
        public string ItemSize { get; set; }
        public string ExternalId { get; set; }
        /// <summary>
        /// ///////////////////// mi9
        /// </summary>
        public string CurrentPrice { get; set; }
        public string RegularPrice { get; set; }
        public string CurrentUnitPrice { get; set; }
        public bool IsAvailable { get; set; }
        public bool InStock { get; set; }
        public List<SizesDto> Sizes { set; get; }
    }
}
