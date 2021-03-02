using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Interfaces
{
    public interface IPurchaseWebApiClient
    {
        /// <summary>
        /// Get Item bought in a period of time
        /// </summary>
        /// <param name="memberInternalKey"></param>
        /// <param name="limit"></param>
        /// <param name="dayOffset"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<ShoppingHistoryPaging> GetItemsIBuyLast90Days(string accessToken, int limit, string fromDate, string toDate, int storeId);
    }
}
