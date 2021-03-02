using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Core.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Services
{
    public class PurchaseService : IPurchaseService
    {
        readonly IPurchaseWebApiClient _purchaseWebApiClient;
        readonly string _accessToken;

        public PurchaseService(IPurchaseWebApiClient purchaseWebApiClient, IOptions<ApplicationSettings> applicationSetting)
        {
            _purchaseWebApiClient = purchaseWebApiClient;
            _accessToken = applicationSetting?.Value?.AccessToken;
        }

        public async Task<ItemsIBuyDto> GetItemsIBuyLast90Days(int limit, int dayOffset, int storeId)
        {
            Func<DateTime, string> convertToString = (DateTime date) => date.ToString("yyyy/MM/dd");

            var fromDate = dayOffset > 0 ? convertToString(DateTime.UtcNow) : null;
            var toDate = dayOffset > 0 ? convertToString((DateTime.UtcNow.AddDays(-dayOffset))) : null;

            var products = await _purchaseWebApiClient.GetItemsIBuyLast90Days(_accessToken, limit, fromDate, toDate, storeId);

            ItemsIBuyDto result = new ItemsIBuyDto
            {
                upcList = products?.Items?.Select(p => p.Sku)?.ToList() ?? new List<string>()
            };

            return result;
        }
    }
}
