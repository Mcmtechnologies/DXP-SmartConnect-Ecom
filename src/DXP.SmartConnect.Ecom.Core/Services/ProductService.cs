using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Entities;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Core.Settings;
using DXP.SmartConnect.Ecom.SharedKernel.Constants;
using DXP.SmartConnect.Ecom.SharedKernel.Helpers;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DXP.SmartConnect.Ecom.Core.Services
{
    public class ProductService : IProductService
    {
        readonly IProductWebApiClient _productWebApiClient;
        readonly IProductRepository _productRepository;
        readonly IMapper _mapper;
        readonly ILogger<ProductService> _logger;

        readonly int MaxProductNeedCaChe;
        readonly string Disclaimer;
        readonly string CloudContentUrl;
        readonly string ImageDimensions;

        const string ProductListCacheKey = "ListProduct";

        public ProductService(IProductWebApiClient productWebApiClient,
            IProductRepository productRepository,
            IMapper mapper,
            IOptionsSnapshot<InMemoryCacheSettings> cacheOption,
            IOptionsSnapshot<ProductSettings> productSetting,
            IOptionsSnapshot<ApplicationSettings> appSettings,
            IOptionsSnapshot<ImageSettings> imageSettings,
            ILogger<ProductService> logger)
        {
            _productWebApiClient = productWebApiClient;
            _productRepository = productRepository;
            _mapper = mapper;
            MaxProductNeedCaChe = cacheOption?.Value?.ProductThreshHoldShouldCache ?? 15;
            _logger = logger;
            Disclaimer = productSetting?.Value?.Disclaimer;
            CloudContentUrl = appSettings?.Value?.CloudContentUrl ?? string.Empty;
            ImageDimensions = imageSettings?.Value?.ProductImageDimensions;
        }

        public async Task<ProductDto> GetProductByUpcAsync(int storeId, string upc)
        {
            var product = await _productWebApiClient.GetProductByUpcAsync(storeId, upc);

            return ConvertToProductDtoFromProductUpc(product);
        }

        public async Task<ProductDto> GetProductByUpcDbAsync(string storeId, string upc)
        {
            var product = await _productRepository.GetProductByUpcAsync(storeId, int.Parse(upc));

            return ProductDto.FromRsProduct(product);
        }

        public async Task<IList<ProductIndexDto>> GetProductsIndexByKeyword(string keyword, int limit, string storeId)
        {
            List<ProductIndexDto> indexes = new List<ProductIndexDto>();
            var searchResult = await _productWebApiClient.SearchProductIndexAsync(storeId, keyword, limit);
            if (searchResult != null && searchResult.Products != null)
            {
                searchResult.Products.ToList().ForEach(p =>
                {
                    indexes.Add(new ProductIndexDto
                    {
                        ITEM_ID = p.ProductId,
                        UPC = p.Sku,
                        ITEM_DESC = p.Brand != null ? $"{p.Brand} {p.Name}" : p.Name,
                    });
                });
            }

            return indexes;
        }

        public async Task<ProductPagingDto> SearchProductPaging(ProductSearchExtendedDto searchProductsRequestDto)
        {
            var productsDto = await this.SearchProductAsync(searchProductsRequestDto);

            if (productsDto != null)
                return _mapper.Map<ProductPagingDto>(productsDto);
            else
                return null;
        }

        public async Task<ProductPagingDto> SearchTopProducts(ProductSearchExtendedDto searchProductsRequestDto)
        {
            ProductUpcPagingDto productsDto = await this.SearchTopProductsAsync(searchProductsRequestDto);

            if (productsDto == null)
                return null;

            ProductPagingDto products = ConvertToProductPaging(productsDto);
            if (products.Products == null)
                return null;

            return products;
        }

        public async Task<ProductPagingDto> SearchProductAsync(ProductSearchExtendedDto searchProductsRequestDto)
        {
            ProductSearch mwgResult;

            if (searchProductsRequestDto.UPCList != null && searchProductsRequestDto.UPCList.Any())
            {
                if (searchProductsRequestDto.UPCList.Count >= MaxProductNeedCaChe)
                {
                    mwgResult = await GetProductsByUpcUseCache(searchProductsRequestDto);
                }
                else
                {
                    var productMultiSearch = await _productWebApiClient.GetProductByMultiUPCAsync(searchProductsRequestDto.ExternalStoreID, searchProductsRequestDto.UPCList);
                    mwgResult = ConvertToProductSearch(productMultiSearch);
                }

                // Paging.
                if (searchProductsRequestDto.PageNum >= 0 && searchProductsRequestDto.PageSize > 0)
                    mwgResult.Items = mwgResult.Items.Skip(searchProductsRequestDto.PageNum * searchProductsRequestDto.PageSize).Take(searchProductsRequestDto.PageSize).ToList();
            }
            else
            {
                if (searchProductsRequestDto.DepartmentId?.Any() == true || searchProductsRequestDto.SubDept1Id?.Any() == true || searchProductsRequestDto.SubDept2Id?.Any() == true)
                {
                    mwgResult = await SearchProductsUseCache(searchProductsRequestDto);
                }
                else
                {
                    mwgResult = await SearchProducts(searchProductsRequestDto);
                }
            }

            if (mwgResult != null && mwgResult.Items != null && mwgResult.Items.Any())
            {
                //Convert data to ProductsPagingDto type
                ProductPagingDto productsDto = new ProductPagingDto
                {
                    PageIndex = searchProductsRequestDto.PageNum,
                    PageSize = searchProductsRequestDto.PageSize
                };
                TransformProductPaging(mwgResult, productsDto);

                return productsDto;
            }
            return null;
        }

        public async Task<DeptPagingDto> GetDeptTopPaging(ProductSearchExtendedDto searchProductsRequestDto)
        {
            ProductUpcPagingDto productsDto = await this.SearchTopProductsAsync(searchProductsRequestDto);
            if (productsDto == null)
                return null;

            DeptPagingDto depts = ConvertToDeptPaging(productsDto);

            return depts;
        }

        private async Task<ProductSearch> GetProductsByUpcUseCache(ProductSearchExtendedDto param)
        {
            try
            {
                var storeId = param.ExternalStoreID;
                //var cacheKey = CacheKeyHelper.GetQueryKey(ProductListCacheKey, storeId, HashHelper.ComputeHash(param.UPCList))

                var mwg8ProductMultiSearch = await _productWebApiClient.GetProductByMultiUPCAsync(storeId, param.UPCList);
                var mwgProducts = ConvertToProductSearch(mwg8ProductMultiSearch);

                return mwgProducts;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetProductsByUpcUseCache - Error", ex?.Message);
                return null;
            }
        }

        private ProductSearch ConvertToProductSearch(ProductMultiSearch productMultiSearch)
        {
            try
            {
                var result = new ProductSearch();
                if (productMultiSearch != null && productMultiSearch.Items.Any())
                {
                    foreach (var item in productMultiSearch.Items)
                    {
                        var product = item.Items.FirstOrDefault();
                        if (product != null)
                            result.Items.Add(product);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ConvertToProductSearch)} - Error", ex?.Message);
                return null;
            }
        }

        private async Task<ProductSearch> SearchProductsUseCache(ProductSearchExtendedDto param)
        {
            try
            {
                var products = await SearchProductsByCategoryId(param);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError("Mwg8SearchProductsUseCache - Error", ex?.Message);
                return null;
            }
        }

        private async Task<ProductSearch> SearchProductsByCategoryId(ProductSearchExtendedDto param)
        {
            // Store.
            var storeId = param.ExternalStoreID;

            // Category.
            var categoryId = "0";
            if (param.SubDept2Id != null && param.SubDept2Id.Any())
            {
                categoryId = param.SubDept2Id.FirstOrDefault().ToString();
            }
            else if (param.SubDept1Id != null && param.SubDept1Id.Any())
            {
                categoryId = param.SubDept1Id.FirstOrDefault().ToString();
            }
            else if (param.DepartmentId != null && param.DepartmentId.Any())
            {
                categoryId = param.DepartmentId.FirstOrDefault().ToString();
            }

            // Paging.
            if (param.PageNum < 0) param.PageNum = 0;
            if (param.PageSize < 1) param.PageSize = 1;
            var skip = param.PageNum * param.PageSize;

            // Sort.
            var sort = param.SortOrder == null || !param.SortOrder.Any() ? string.Empty : param.SortOrder.FirstOrDefault()?.sortColumn;

            // Filter with Brand Name.
            string brandQuery = GetFilterQuery(ProductSearchExtendedDto.Brand, param.BrandName);
            // Filter with Category Id (Department Id in RS).
            string categoryQuery = GetFilterQuery(ProductSearchExtendedDto.Category, param.DepartmentName);
            // Filter with Savings Name.
            string savingsQuery = GetFilterQuery(ProductSearchExtendedDto.OnSale, param.SavingsName);
            // Filter final.
            string filterQuery = $"{brandQuery}{categoryQuery}{savingsQuery}";
            if (!string.IsNullOrEmpty(filterQuery))
            {
                filterQuery = filterQuery.Substring(1);
            }

            // Search product service do not support and multi filter at this time.
            var products = await _productWebApiClient.SearchProductByCategoryAsync(storeId, categoryId, skip, param.PageSize, sort, filterQuery);

            return products;
        }

        private string GetFilterQuery<T>(string type, IList<T> values)
        {
            var query = new StringBuilder();
            if (values != null && values.Any())
            {
                foreach (T item in values)
                {
                    query.Append($"{type}:{HttpUtility.UrlEncode(item.ToString())}");
                }
            }
            if (query.Length > 0)
                query.Insert(0, ",");

            return query.ToString();
        }

        private async Task<ProductSearch> SearchProducts(ProductSearchExtendedDto param)
        {
            // Store
            var storeId = param.StoreID;

            // Paging.
            if (param.PageNum < 0) param.PageNum = 0;
            if (param.PageSize < 1) param.PageSize = 1;
            var skip = param.PageNum * param.PageSize;

            // Sort.
            var sort = param.SortOrder == null || !param.SortOrder.Any() ? string.Empty : param.SortOrder.FirstOrDefault()?.sortColumn;

            // Filter with Brand Name.
            string brandQuery = GetFilterQuery(ProductSearchExtendedDto.Brand, param.BrandName);
            // Filter with Category Id (Department Id in RS).
            string categoryQuery = GetFilterQuery(ProductSearchExtendedDto.Category, param.DepartmentName);
            // Filter with Savings Name.
            string savingsQuery = GetFilterQuery(ProductSearchExtendedDto.OnSale, param.SavingsName);
            // Filter final.
            string filterQuery = $"{brandQuery}{categoryQuery}{savingsQuery}";
            if (!string.IsNullOrEmpty(filterQuery))
            {
                filterQuery = filterQuery.Substring(1);
            }

            // Search product service do not support and multi filter at this time.
            var products = await _productWebApiClient.SearchProductAsync(storeId.ToString(), param.Keyword, skip, param.PageSize, sort, false, filterQuery);

            return products;
        }

        private void TransformProductPaging(ProductSearch from, ProductPagingDto to)
        {
            try
            {
                // Paging.
                to.TotalRecords = from.Total > 0 ? from.Total : from.Items.Count;
                to.TotalPages = Convert.ToInt32(Math.Ceiling((Double)to.TotalRecords / to.PageSize));

                // Category, Brand, Sale filter. 
                TransformProductFilter(from, to);

                // Products.
                to.Products = new List<ProductDto>();
                foreach (Product item in from.Items)
                {
                    ProductDto product = ConvertToProductDto(item);
                    to.Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(TransformProductPaging)} - Error", ex?.Message);
            }
        }

        private void TransformProductFilter(ProductSearch from, ProductPagingDto to)
        {
            try
            {
                if (from.Facets != null)
                {
                    IList<ProductFacet> brands;
                    IList<ProductFacet> categories;
                    //IList<ProductFacet> dietaries
                    //IList<ProductFacet> sales

                    if (from.Facets.TryGetValue(ProductSearchExtendedDto.Brand, out brands))
                    {
                        to.BrandSummary = brands.Select(b => b.Value).ToList();
                    }
                    if (from.Facets.TryGetValue(ProductSearchExtendedDto.Category, out categories))
                    {
                        to.DeptSummary = categories.Select(c => new DeptSummaryDto { Department = c.Value }).ToList();
                    }
                    // OnSale and Dietaries do not existed on ProductsPagingDto.
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(TransformProductFilter)} - Error", ex?.Message);
            }
        }

        private ProductDto ConvertToProductDto(Product item)
        {
            try
            {
                var product = new ProductDto
                {
                    Id = item.Sku,
                    Name = item.Brand != null ? $"{item.Brand} {item.Name}" : item.Name,
                    FullDescription = item.Description,
                    //Ingredients = ing // Mi9v8 not existed.
                    UnitPrice = item.PricePerUnit,
                    PriceText = item.Price,
                    OnSale = item.WasPrice != null,
                    SalePriceText = item.WasPrice,
                    UPC = item.Sku,
                    ItemKey = item.Sku,
                    Disclaimer = Disclaimer,
                    Available = item.Available,
                    AvailableInStore = item.Available,
                    Category = item.DefaultCategory?.FirstOrDefault()?.Category,
                    DefaultImage = $"{CloudContentUrl}/DefaultMissingImage.jpg",
                    ProductCategories = new List<ProductCategoryDto>()
                    {
                        new ProductCategoryDto()
                        {
                            CategoryId = item.DefaultCategory?.FirstOrDefault()?.RetailerId,
                            CategoryName = item.DefaultCategory?.FirstOrDefault()?.Category
                        }
                    },
                    ProductVariants = new List<ProductVariantDto>
                    {
                        new ProductVariantDto
                        {
                            Name = item.Name,
                            ProductId = item.ProductId,
                            Id = item.ProductId, // Variant ID as Product Id aka Sku.
                            Description = item.Description,
                            ProductName = item.Name,
                            Sku = item.Sku
                        }
                    },
                };

                // Sizes.
                List<SizeDto> productSizes = new List<SizeDto>();
                if (item.UnitOfSize != null)
                {
                    productSizes.Add(new SizeDto
                    {
                        Size = item.UnitOfSize.Size.ToString(),
                        ItemKey = item.Sku
                    });
                    product.Sizes = productSizes;
                }
                // Ingredients (not existed Mi9v8).

                // Image.
                string imageUrl = "";
                if (item.Image != null && item.Image.TryGetValue(ImageDimensions, out imageUrl))
                {
                    product.DefaultImage = imageUrl;
                }

                // Attribute.
                //string productType = string.Empty
                if (item.Attributes != null && item.Attributes.TryGetValue(ProductAttributeConstants.ItemType, out object prodTypeObj))
                {
                    //product.ProductType = productType
                    product.ProductType = prodTypeObj.ToString();
                }
                //string totalInStockTxt = string.Empty
                if (item.Attributes != null && item.Attributes.TryGetValue(ProductAttributeConstants.TotalOnHandQty, out object totalInStockObj))
                {
                    int totalInStock;
                    //if (int.TryParse(totalInStockTxt, out totalInStock))
                    if (int.TryParse(totalInStockObj.ToString(), out totalInStock))
                    {
                        product.InStock = totalInStock > 0;
                    }
                }

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ConvertToProductDto)} - Error", ex?.Message);
                return null;
            }
        }

        private ProductDto ConvertToProductDtoFromProductUpc(ProductUpc item)
        {
            var productDto = ConvertToProductDto(item);
            productDto.DefaultCategory = item.DefaultCategory;

            if (item.PrimaryImage != null && item.PrimaryImage.TryGetValue(ImageDimensions, out string imageUrl))
            {
                productDto.DefaultImage = imageUrl;
            }

            return productDto;
        }

        public async Task<ProductUpcPagingDto> SearchTopProductsAsync(ProductSearchExtendedDto searchProductsRequestDto)
        {
            // Init params.
            var storeId = searchProductsRequestDto.StoreID.ToString();
            var categoryId = searchProductsRequestDto.SubDept1Id?.Any() == true ? searchProductsRequestDto.SubDept1Id.FirstOrDefault() : searchProductsRequestDto.DepartmentId?.FirstOrDefault();
            var limit = searchProductsRequestDto.TopItemsNum ?? 5; // Default 5 products each.
            var searchResult = await _productWebApiClient.SearchTopProductAsync(storeId, categoryId?.ToString(), limit);

            if (searchResult != null && searchResult.SubCategories != null)
            {
                var productsDto = new ProductUpcPagingDto();

                foreach (var item in searchResult.SubCategories)
                {
                    if (item.Value?.Items == null)
                        continue;
                    var mwgSummary = new MhSummaryDto();

                    foreach (var mwgProduct in item.Value?.Items)
                    {
                        var productDto = ConvertToProductDto(mwgProduct);
                        if (productDto != null)
                        {
                            mwgSummary.Products.Add(productDto);
                        }
                    }
                    mwgSummary.departmentId = item.Value.RetailerCategoryId;
                    mwgSummary.department = item.Key;
                    mwgSummary.subDept1s.Add(
                        new SubDept1sDto
                        {
                            SubDept1Id = item.Value?.Items.First()?.Categories[2]?.RetailerId,
                            SubDepartment1 = item.Value?.Items.First()?.Categories[2]?.Category,
                            SubDept1Parent = item.Value?.Items.First()?.Categories[1]?.RetailerId
                        });

                    productsDto.mhSummary.Add(mwgSummary);
                }

                productsDto.mwgSummary = new List<DeptSummaryDto>
                {
                    new DeptSummaryDto
                    {
                        Department = searchResult.Title,
                        DepartmentId = searchResult.RetailerCategoryId,
                        SubDept1s = productsDto.mhSummary.Select(s => new SubDept1sDto
                        {
                            SubDepartment1 = s.department,
                            SubDept1Id = s.departmentId
                        }).ToList()
                    }
                };

                return productsDto;
            }

            return null;
        }

        private DeptPagingDto ConvertToDeptPaging(ProductUpcPagingDto from)
        {
            DeptPagingDto to = new DeptPagingDto();
            to.Depts = new List<DeptDto>();
            foreach (var dept in from.mhSummary)
            {
                var deptToAdd = new DeptDto()
                {
                    DeptName = dept.department,
                    DeptId = dept.departmentId,
                    Products = _mapper.Map<List<ProductDto>>(dept.Products)
                };
                to.Depts.Add(deptToAdd);
            }

            to.DeptSummary = from.mwgSummary;

            return to;
        }

        private ProductPagingDto ConvertToProductPaging(ProductUpcPagingDto from)
        {
            ProductPagingDto to = new ProductPagingDto();

            List<ProductDetailDto> productForm;

            if (from.page != null)
            {
                productForm = from.page.content.ToList();
                to.TotalPages = from.page.totalPages;
                to.TotalRecords = from.page.totalElements;
                to.PageSize = from.page.pageSize;
                to.PageIndex = from.page.pageNumber;
            }
            else
            {
                productForm = from.responseList?.ToList() ?? new List<ProductDetailDto>();
            }


            //Check Product Sale has the REG_PRICE_MULTI value matches the TPR_PRICE_MULTI and the REG_PRICE matches the TPR_PRICE
            //Do not display the REG_PRICE.
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            to.Products = productForm.Select(p => new ProductDto()
            {
                Id = p.itemId,
                Name = textInfo.ToTitleCase(p.gfItemDescription.ToLower()),
                FullDescription = p.gfItemDescription,
                ProductType = p.nameForSales,
                Price = p.tprPrice != null && (p.regPrice.Equals(p.tprPrice) && ((string.IsNullOrEmpty(p.tprPriceMulti) && p.regPriceMulti == "1") || p.regPriceMulti.Equals(p.tprPriceMulti))) ? 0 : p.regPrice,
                PriceText = p.tprPrice != null && (p.regPrice.Equals(p.tprPrice) && ((string.IsNullOrEmpty(p.tprPriceMulti) && p.regPriceMulti == "1") || p.regPriceMulti.Equals(p.tprPriceMulti))) ? "" : ConvertPrice(p.regPrice),
                ItemSize = !string.IsNullOrEmpty(p.unitSize) ? p.unitSize.ToLower() : p.unitSize,
                SalePrice = p.tprPrice,
                SalePriceText = ConvertPrice(p.tprPrice),
                OnSale = p.tprPrice != null,
                UPC = p.upc,
                ExternalId = p.mwgProductGuid,
                Category = p.department,
                DefaultImage = string.IsNullOrEmpty(p.DefaultImage) ? CloudContentUrl + "/DefaultMissingImage.jpg" : p.DefaultImage,
                ProductCategories = new List<ProductCategoryDto>()
                {
                    new ProductCategoryDto()
                    {
                        CategoryId = p.departmentId.ToString(),
                        CategoryName = p.department
                    }
                },
                ProductAttributes = p.itemAttributes.Where(e => e.id.corCustomField.tabLabel.Equals("Additional Info2") && e.fieldValue == "Y").Select(s => new ProductAttributeDto()
                {
                    AtributeID = s.id.corCustomField.fieldId.ToString(),
                    AttributeName = textInfo.ToTitleCase(s.id.corCustomField.fieldLabel.ToLower())
                }).ToList()
            }).ToList();

            if (from.mhSummary != null)
            {
                to.DeptSummary = from.mhSummary.Select(s => new DeptSummaryDto()
                {
                    DepartmentId = s.departmentId,
                    Department = s.department,
                    SubDept1s = s.subDept1s.Select(l => new SubDept1sDto()
                    {
                        SubDept1Id = l.SubDept1Id,
                        SubDepartment1 = l.SubDepartment1,
                        SubDept1Parent = l.SubDept1Parent,
                        SubDept2s = l.SubDept2s.Select(m => new SubDept2sDto()
                        {
                            SubDept2Id = m.SubDept2Id,
                            SubDepartment2 = m.SubDepartment2,
                            SubDept2Parent = m.SubDept2Parent,
                        }).ToList()
                    }).ToList()
                }).ToList();
            }

            if (from.attributeSummary != null)
            {
                to.AttributeSummary = from.attributeSummary.Select(b => new AttributeSummaryDto()
                {
                    FieldId = b.FieldId,
                    FieldLabel = textInfo.ToTitleCase(b.FieldLabel.ToLower()),
                    FieldTypeId = b.FieldTypeId,
                    FieldValue = b.FieldValue,
                    GroupLabel = b.GroupLabel,
                    TabLabel = b.TabLabel,
                    ViewToolTip = b.ViewToolTip,
                    EditToolTip = b.EditToolTip,
                    Status = b.Status,
                    DynamicLabel = b.DynamicLabel,
                }).ToList();
            }
            if (from.brandSummary != null)
            {
                to.BrandSummary = from.brandSummary.Select(b => textInfo.ToTitleCase(b.ToLower())).ToList();
            }
            else
            {
                to.BrandSummary = from.brandSummary;
            }

            return to;
        }

        private string ConvertPrice(double? value)
        {
            string priceType = string.Empty;
            return String.Format("{0:C}{1}", value, priceType);
        }
    }
}
