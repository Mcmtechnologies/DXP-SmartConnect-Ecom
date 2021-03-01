using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.API.Controllers
{
    public class InventoryController : BaseApiController
    {
        private readonly IProductService _productService;

        public InventoryController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Search Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("product/search/v5")]
        public async Task<ProductPagingV5Dto> SearchProducts(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.SearchProductsV5(searchProductsRequestRo);
        }

        /// <summary>
        /// Get product by upc
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="upc"></param>
        /// <returns></returns>
        [HttpGet("product/get-by-upc")]
        public async Task<ProductDto> SearchProductByUpc(int storeId, string upc)
        {
            return await _productService.GetProductByUpcAsync(storeId, upc);
        }

        /// <summary>
        /// Search Top Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("product/search/top/v5")]
        public async Task<ProductPagingV5Dto> SearchTopProducts(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.SearchTopProducts(searchProductsRequestRo);
        }

        /// <summary>
        /// Search Top Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("product/dept/top/v5")]
        public async Task<DeptPagingv5Dto> GetDeptTopPagingV5(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.GetDeptTopPagingV5(searchProductsRequestRo);
        }

        /// <summary>
        /// Search product index by keyword
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <param name="limit">Item Quantity</param>
        /// <param name="storeId">Store ID</param>
        /// <returns></returns>
        [HttpGet("productIndex/search")]
        public async Task<IList<ProductIndexDto>> GetProductsIndexByKeyword(string keyword, int limit, string storeId)
        {
            return await _productService.GetProductsIndexByKeyword(keyword, limit, storeId);
        }
    }
}
