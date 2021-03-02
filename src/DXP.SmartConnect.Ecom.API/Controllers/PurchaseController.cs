using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.API.Controllers
{
    public class PurchaseController : BaseApiController
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Get Item bought in a period of time
        /// </summary>
        /// <param name="memberInternalKey"></param>
        /// <param name="limit"></param>
        /// <param name="dayOffset"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet("itemibuy/get")]
        public async Task<ItemsIBuyDto> GetItemsIBuyLast90Days(int memberInternalKey, int limit, int dayOffset, int storeId)
        {
            return await _purchaseService.GetItemsIBuyLast90Days(limit, dayOffset, storeId);
        }
    }
}
