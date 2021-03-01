using System;
using System.Collections.Generic;
using System.Text;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class ProductSearchDto //ProductRequestRO
    {
        public string CategoryId { get; set; }
        public string UserID { get; set; }
        public string CompanyId { get; set; }
        public string StoreID { get; set; }
        public string AisleID { get; set; }
        public string Keyword { get; set; }
        public int? Status { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string SortExp { get; set; }
        public string ProductType { get; set; }
        public bool? OnSale { get; set; }
        public string Sku { get; set; }
        public string Filter { get; set; }
    }

    public class ProductSearchExtendedDto //SearchProductRequestDto
    {
        public const string Category = "Category";
        public const string Brand = "Brand";
        public const string Dietary = "Bietary";
        public const string OnSale = "On Sale";

        public string Keyword { get; set; }
       
        public string ExternalStoreID { get; set; }
       
        public int StoreID { get; set; }
       
        public int PageSize { get; set; }
       
        public int PageNum { get; set; }
       
        public List<string> UPCList { get; set; }
       
        public bool? CheckSumUPC { get; set; }
       
        public List<ProductSortOrderDto> SortOrder { get; set; }
       
        public List<string> SavingsName { get; set; }
       
        public List<string> BrandName { get; set; }
       
        public List<string> DepartmentName { get; set; }
       
        public List<string> DepartmentId { get; set; }
       
        public List<string> SubDept1Id { get; set; }
       
        public List<string> SubDept2Id { get; set; }
       
        public List<int> ItemId { get; set; }
       
        public List<int> ItemNum { get; set; }
       
        public string TprType { get; set; }
       
        public string PromoInd { get; set; }
       
        public string Status { get; set; }
       
        public List<CustomFieldsDto> CustomFields { get; set; }
       
        public bool DisplayMhSummary { get; set; }
       
        public bool IncludedSecondary { get; set; }
       
        public bool DisplayAttributeSummary { get; set; }
       
        public bool DisplayBrandSummary { get; set; }
       
        public bool? PrimaryBarcode { get; set; }
       
        public int? TopItemsNum { get; set; }
    }
}
