using DXP.SmartConnect.Ecom.Core.Entities;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Infrastructure.Constants;
using DXP.SmartConnect.Ecom.SharedKernel.WebApi;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Infrastructure.Data.WebApiClients
{
    public class OrderWebApiClient : WebApiClient, IOrderWebApiClient
    {
        private readonly ILogger<OrderWebApiClient> _logger;

        public OrderWebApiClient(ILogger<OrderWebApiClient> logger, HttpClient client) : base(logger, client)
        {
            _logger = logger;
        }

        public Task<bool> CancelOrder(string accessToken, string referenceId)
        {
            var path = $"orders/{referenceId}";

            var header = new Dictionary<string, string>();
            header.Add(WebApiClientConstants.HeaderContentType, WebApiClientOrderConstants.CancelOrder);

            return PostAsync<bool>(path, HttpMethod.Post, null, accessToken, header, HttpStatusSuccessCodes.OrderChange);
        }

        public Task<Order> GetOrderByReference(string accessToken, string referenceId)
        {
            var path = $"orders/{referenceId}";
            return GetAsync<Order>(path, accessToken, null, HttpStatusSuccessCodes.GetOrder);
        }

        public Task<Order> GetOrderInstoreByreference(string accessToken, string referenceId)
        {
            var path = $"/api/instoreorders/{referenceId}";
            return GetAsync<Order>(path, accessToken, null, HttpStatusSuccessCodes.GetOrder);
        }

        public Task<OrderPage> GetOrders(string accessToken, string fulfilment, string from, string to, int skip = 0, int take = 10)
        {
            var fulfilmentPath = string.IsNullOrEmpty(fulfilment) ? "" : $"&fulfilment={fulfilment}";
            var fromPath = string.IsNullOrEmpty(from) ? "" : $"&from={from}";
            var toPath = string.IsNullOrEmpty(to) ? "" : $"&to={to}";
            var skipPath = $"?skip={skip}";
            var takePath = $"&take={take}";
            var path = $"orders{skipPath}{takePath}{fromPath}{toPath}{fulfilmentPath}";

            return GetAsync<OrderPage>(path, accessToken, null, HttpStatusSuccessCodes.GetOrder);
        }

        public Task<OrderPage> GetOrdersInstore(string accessToken, string from, int skip, int take)
        {
            var fromPath = string.IsNullOrEmpty(from) ? "" : $"&from={from}";
            var skipPath = $"?skip={skip}";
            var takePath = $"&take={take}";
            var path = $"instoreorders{skipPath}{takePath}{fromPath}";

            return GetAsync<OrderPage>(path, accessToken, null, HttpStatusSuccessCodes.GetOrder);
        }
    }
}
