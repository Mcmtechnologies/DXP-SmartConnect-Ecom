using DXP.SmartConnect.Ecom.Core.Entities;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IOrderWebApiClient
    {
        /// <summary>
        /// Get list order.
        /// </summary>
        /// <param name="fulfilment">Fulfilment </param>
        /// <param name="from">From date </param>
        /// <param name="to">To date </param>
        /// <param name="skip">Page index </param>
        /// <param name="take">Page size </param>
        /// <returns>List order</returns>
        Task<OrderPage> GetOrders(string accessToken, string fulfilment, string from, string to, int skip, int take);
        /// <summary>
        /// Get order by reference id.
        /// </summary>
        /// <param name="referenceId">Order referenceId </param>
        /// <returns>Order detail</returns>
        Task<Order> GetOrderByReference(string accessToken, string referenceId);
        /// <summary>
        /// Get list order instore.
        /// </summary>
        /// <param name="from">From date </param>
        /// <param name="skip">Page index </param>
        /// <param name="take">Page size </param>
        /// <returns>List order</returns>
        Task<OrderPage> GetOrdersInstore(string accessToken, string from, int skip, int take);
        /// <summary>
        /// Get order instore by reference id.
        /// </summary>
        /// <param name="referenceId">Order referenceId </param>
        /// <returns>Order detail</returns>
        Task<Order> GetOrderInstoreByreference(string accessToken, string referenceId);
        /// <summary>
        /// Cancel order.
        /// </summary>
        /// <param name="referenceId">Order referenceId </param>
        /// <returns>Status of command</returns>
        Task<bool> CancelOrder(string accessToken, string referenceId);

    }
}
