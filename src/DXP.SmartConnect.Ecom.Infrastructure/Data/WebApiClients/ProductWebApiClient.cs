using DXP.SmartConnect.Ecom.Core.Entities;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.SharedKernel.WebApi;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients
{
    public class ProductWebApiClient : WebApiClient, IProductWebApiClient
    {
        private static readonly HttpStatusCode[] _productHttpStatusCodesSuccessfully = {
           HttpStatusCode.OK, // 200
           HttpStatusCode.NoContent, // 204
           HttpStatusCode.NotFound // 404
        };

        private readonly ILogger<ProductWebApiClient> _logger;

        public ProductWebApiClient(ILogger<ProductWebApiClient> logger, HttpClient client) : base(logger, client)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get product by upc.
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="upc">The upc of product </param>
        /// <returns>Product detail</returns>
        public async Task<ProductUpc> GetProductByUpcAsync(int storeId, string upc)
        {
            var path = $"/api/stores/{storeId}/products/{upc}";
            return await GetAsync<ProductUpc>(path);
        }

        /// <summary>
        /// Get products by multi upc.
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="upcs">List upc of products </param>
        /// <param name="skip">Number of products to skip </param>
        /// <param name="take">Number of products to return </param>
        /// <returns>Product detail</returns>
        /// <remarks>
        /// We just need get 1 product for each upc, so default skip = 0 and take = 1 is good for this. 
        /// </remarks>
        public async Task<ProductMultiSearch> GetProductByMultiUPCAsync(string storeId, IList<string> upcs, int skip = 0, int take = 1)
        {
            StringBuilder listUpcPath = new StringBuilder();
            foreach (string upc in upcs)
            {
                listUpcPath.Append("&q=").Append(upc);
            }
            var path = $"/api/stores/{storeId}/multisearch?skip={skip}&take={take}{listUpcPath.ToString()}";
            return await GetAsync<ProductMultiSearch>(path);
        }

        /// <summary>
        /// Search product index (preview).
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="keyword">Search item keyword </param>
        /// <param name="popularTake">Number of popular products to return</param>
        /// <param name="productsTake">Number of products to return</param>
        /// <param name="categoriesTake">Number of categiories to return</param>
        /// <returns>Product index search result</returns>
        public async Task<ProductIndex> SearchProductIndexAsync(string storeId, string keyword, int productsTake, int? popularTake = null, int? categoriesTake = null)
        {
            var popularTakePath = popularTake != null ? $"&popularTake={productsTake}" : "";
            var categoriesTakePath = categoriesTake != null && categoriesTake > 0 ? $"&categoriesTake={categoriesTake}" : "";

            var path = $"/api/stores/{storeId}/preview?q={keyword}&productsTake={productsTake}{popularTakePath}{categoriesTakePath}";

            return await GetAsync<ProductIndex>(path);
        }

        /// <summary>
        /// Search top number of product in subcategories.
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="categoryId">Category Retailer Id </param>
        /// <param name="take">Number of documents to return</param>
        /// <returns>Product search result</returns>
        public async Task<ProductTop> SearchTopProductAsync(string storeId, string categoryId, int take)
        {
            var path = $"/api/stores/{storeId}/categories/{categoryId}/subcategories/search?take={take}";
            return await GetAsync<ProductTop>(path);
        }

        /// <summary>
        /// Search product by query.
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="keyword">Search item keyword </param>
        /// <param name="skip">Number of documents to skip</param>
        /// <param name="take">Number of documents to return</param>
        /// <param name="sort">Sort expression - available value: brand, brand+desc, default, price, price+desc </param>
        /// <param name="misspelling">Search with misspelling keyword. Default is false</param>
        /// <param name="filters">Filters - e.g.: Brand:SILK,Category:MILK SUBSTITUTES </param>
        /// <param name="facets">Facets - available value: brand, category, dietary, on Sale </param> 
        /// <returns>Product search result</returns>
        public async Task<ProductSearch> SearchProductAsync(string storeId,
                                               string keyword,
                                               int skip,
                                               int take,
                                               string sort = null,
                                               bool misspelling = false,
                                               string filters = null,
                                               string facets = null)
        {
            var sortPath = sort != null ? $"&sort={sort}" : "";
            var misspellingPath = misspelling ? $"&misspelling={misspelling}" : "";
            var filtersPath = filters != null ? $"&f={filters}" : "";
            var facetsPath = facets != null ? $"&a={facets}" : "";

            var path = $"/api/stores/{storeId}/search?q={keyword}&skip={skip}&take={take}{sortPath}{misspellingPath}{filtersPath}{facetsPath}";

            return await GetAsync<ProductSearch>(path);
        }

        /// <summary>
        /// Search product by category (Id).
        /// </summary>
        /// <param name="storeId">Retailer Store Id </param>
        /// <param name="categoryId">Category Retailer Id </param>
        /// <param name="skip">Number of documents to skip</param>
        /// <param name="take">Number of documents to return</param>
        /// <param name="sort">Sort expression - available value: brand, brand+desc, default, price, price+desc </param>
        /// <param name="filters">Filters - e.g.: brand:SILK,Category:MILK SUBSTITUTES </param>
        /// <param name="facets">Facets - available value: brand, category, dietary, on Sale </param> 
        /// <returns>Product search result</returns>
        public async Task<ProductSearch> SearchProductByCategoryAsync(string storeId,
                                                         string categoryId,
                                                         int skip,
                                                         int take,
                                                         string sort = null,
                                                         string filters = null,
                                                         string facets = null)
        {
            var sortPath = sort != null ? $"&sort={sort}" : "";
            var filtersPath = filters != null ? $"&f={filters}" : "";
            var facetsPath = facets != null ? $"&a={facets}" : "";

            var path = $"/api/stores/{storeId}/categories/{categoryId}/search?skip={skip}&take={take}{sortPath}{filtersPath}{facetsPath}";

            return await GetAsync<ProductSearch>(path);
        }
    }
}
