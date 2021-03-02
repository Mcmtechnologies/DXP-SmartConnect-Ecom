using DXP.SmartConnect.Ecom.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Get list orders.
        /// </summary>
        /// <returns>List orders</returns>
        Task<OrderDto> GetOrders();
        /// <summary>
        /// Get list orders instore.
        /// </summary>
        /// <param name="memberinternalkey">Member internal key </param>
        /// <param name="limit">Limit </param>
        /// <param name="detail">Detail </param>
        /// <returns>List orders</returns>
        Task<IList<OrderInstoreDto>> GetOrdersInstore(int memberinternalkey, int limit, int detail);
        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="referenceId">Order reference Id </param>
        /// <param name="cardId">Card Id </param>
        /// <returns>Order detail</returns>
        Task<OrderInfoDto> GetOrderById(string referenceId);
        /// <summary>
        /// Get order instore by id.
        /// </summary>
        /// <param name="referenceId">Order reference Id </param>
        /// <param name="cardId">Card Id </param>
        /// <returns>Order instore detail</returns>
        Task<OrderInstoreDto> GetOrderInstoreById(string referenceId);
        /// <summary>
        /// Cancel order.
        /// </summary>
        /// <param name="referenceId">Order reference Id </param>
        /// <returns>Order detail</returns>
        Task<bool> CancelOrder(string referenceId);
    }
}
