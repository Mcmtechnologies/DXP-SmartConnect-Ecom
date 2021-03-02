using DXP.SmartConnect.Ecom.Core.Entities;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Infrastructure.Constants;
using DXP.SmartConnect.Ecom.SharedKernel.WebApi;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients
{
    public class PurchaseWebApiClient : WebApiClient, IPurchaseWebApiClient
    {
        public PurchaseWebApiClient(ILogger<PurchaseWebApiClient> logger, HttpClient client) : base(logger, client)
        {
        }

        public async Task<ShoppingHistoryPaging> GetItemsIBuyLast90Days(string accessToken, int limit, string fromDate, string toDate, int storeId)
        {
            string path = $"/api/stores/{storeId}/lists/shoppinghistory?q=*&take={limit}&skip=0&page=1";

            if (!string.IsNullOrWhiteSpace(fromDate))
            {
                path += $"FromDate={fromDate}";
            }

            if (!string.IsNullOrWhiteSpace(toDate))
            {
                path += $"ToDate={toDate}";
            }

            return await GetAsync<ShoppingHistoryPaging>(path, accessToken, null);
        }
    }
}
