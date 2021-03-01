using DXP.SmartConnect.Ecom.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IProductWebApiClient
    {
        Task<ProductUpc> GetProductByUpcAsync(int storeId, string upc);

        Task<ProductMultiSearch> GetProductByMultiUPCAsync(string storeId, IList<string> upcs, int skip = 0, int take = 1);

        Task<ProductIndex> SearchProductIndexAsync(string storeId, string keyword, int productsTake, int? popularTake = null, int? categoriesTake = null);

        Task<ProductSearch> SearchProductAsync(string storeId,
                                               string keyword,
                                               int skip,
                                               int take,
                                               string sort = null,
                                               bool misspelling = false,
                                               string filters = null,
                                               string facets = null);

        Task<ProductSearch> SearchProductByCategoryAsync(string storeId,
                                                         string categoryId,
                                                         int skip,
                                                         int take,
                                                         string sort = null,
                                                         string filters = null,
                                                         string facets = null);

        Task<ProductTop> SearchTopProductAsync(string storeId, string categoryId, int take);
    }
}
