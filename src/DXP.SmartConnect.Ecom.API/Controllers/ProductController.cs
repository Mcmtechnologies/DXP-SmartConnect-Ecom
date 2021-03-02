using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Search Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("search")]
        public async Task<ProductPagingDto> SearchProducts(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.SearchProductPaging(searchProductsRequestRo);
        }

        /// <summary>
        /// Get product by upc
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="upc"></param>
        /// <returns></returns>
        [HttpGet("get-by-upc")]
        public async Task<ProductDto> SearchProductByUpc(int storeId, string upc)
        {
            return await _productService.GetProductByUpcAsync(storeId, upc);
        }

        /// <summary>
        /// Search Top Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("search/top")]
        public async Task<ProductPagingDto> SearchTopProducts(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.SearchTopProducts(searchProductsRequestRo);
        }

        /// <summary>
        /// Search Top Products
        /// </summary>
        /// <param name="searchProductsRequestRo"></param>
        /// <returns></returns>
        [HttpPost("dept/top")]
        public async Task<DeptPagingDto> GetDeptTopPaging(ProductSearchExtendedDto searchProductsRequestRo)
        {
            return await _productService.GetDeptTopPaging(searchProductsRequestRo);
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
