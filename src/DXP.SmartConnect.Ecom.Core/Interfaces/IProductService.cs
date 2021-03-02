using DXP.SmartConnect.Ecom.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetProductByUpcAsync(int storeId, string upc);
        Task<ProductDto> GetProductByUpcDbAsync(string storeId, string upc);
        Task<IList<ProductIndexDto>> GetProductsIndexByKeyword(string keyword, int limit, string storeId);
        Task<DeptPagingDto> GetDeptTopPaging(ProductSearchExtendedDto searchProductsRequestRo);
        Task<ProductPagingDto> SearchProductPaging(ProductSearchExtendedDto searchProductsRequestDto);
        Task<ProductPagingDto> SearchTopProducts(ProductSearchExtendedDto searchProductsRequestRo);
    }
}
