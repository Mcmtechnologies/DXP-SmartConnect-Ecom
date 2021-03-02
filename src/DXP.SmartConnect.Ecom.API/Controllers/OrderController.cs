using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get orders history
        /// </summary>
        /// <param name="userId">UserName (no need for v8)</param>
        /// <param name="storeId">Store Number (no need for v8)</param>
        /// <returns>Order history</returns>
        // GET: ​api/order/history
        [HttpGet("history")]
        public async Task<OrderDto> GetOrders(string userId, string storeId)
        {
            return await _orderService.GetOrders();
        }

        /// <summary>
        /// Get Order Detail
        /// </summary>
        /// <param name="userId">no need for v8</param>
        /// <param name="orderId">Order reference Id</param>
        /// <returns>order detail</returns>
        // GET: ​api/order
        [HttpGet("")]
        public async Task<OrderInfoDto> GetOrder(string userId, string orderId)
        {
            return await _orderService.GetOrderById(orderId);
        }

        /// <summary>
        /// Cancel Order
        /// </summary>
        /// <param name="userId">no need for v8</param>
        /// <param name="orderId">Order reference Id</param>
        /// <returns>Status of command</returns>
        // POST: ​api/order/cancel
        [HttpPost("cancel")]
        public async Task<bool> CancelOrder(string userId, string orderId)
        {
            return await _orderService.CancelOrder(orderId);
        }

        /// <summary>
        /// Get All Purchases in store By member internal key
        /// </summary>
        /// <param name="memberinternalkey">Member internal key</param>
        /// <param name="limit">Limit</param>
        /// <param name="detail">Detail</param>
        /// <returns>Purchases list</returns>
        // GET: ​api/purchases/instoreV2/memberinternalkey/
        [HttpGet("/api/purchases/instoreV2/memberinternalkey/{memberinternalkey}")]
        public async Task<IList<OrderInstoreDto>> GetPurchasesInStoreByCardId(int memberinternalkey, int limit, int detail)
        {
            return await _orderService.GetOrdersInstore(memberinternalkey, limit, detail);
        }

        /// <summary>
        /// Get Purchase Details By Id
        /// </summary>
        /// <param name="orderId">Order reference Id</param>
        /// <returns>Purchase</returns>
        // GET: ​api/purchases/details/Id/
        [HttpGet("/api/purchases/details/Id/{Id}")]
        public async Task<OrderInstoreDto> GetPurchaseDetailsById(string orderId)
        {
            return await _orderService.GetOrderInstoreById(orderId);
        }
    }
}
