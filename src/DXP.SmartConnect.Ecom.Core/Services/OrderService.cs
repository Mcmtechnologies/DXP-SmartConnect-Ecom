using DXP.SmartConnect.Ecom.Core.DTOs;
using DXP.SmartConnect.Ecom.Core.Events;
using DXP.SmartConnect.Ecom.Core.Interfaces;
using DXP.SmartConnect.Ecom.Core.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DXP.SmartConnect.Ecom.Core.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IOrderWebApiClient _orderWebApiClient;
        private readonly IMediator _mediator;
        private readonly ApplicationSettings _setting;
        private readonly AmazonSQSSettings _sqsSetting;

        public OrderService(
            IOrderWebApiClient orderWebApiClient,
            IMediator mediator,
            IOptions<ApplicationSettings> options,
            IOptions<AmazonSQSSettings> sqsOptions)
        {
            _orderWebApiClient = orderWebApiClient;
            _mediator = mediator;
            _setting = options.Value;
            _sqsSetting = sqsOptions.Value;
        }

        public async Task<bool> CancelOrder(string referenceId)
        {
            return await _orderWebApiClient.CancelOrder(_setting.AccessToken, referenceId);
        }

        public async Task<OrderInfoDto> GetOrderById(string referenceId)
        {
            var order = await _orderWebApiClient.GetOrderByReference(_setting.AccessToken, referenceId);

            return OrderInfoDto.FromOrder(order);
        }

        public async Task<OrderDto> GetOrders()
        {
            var skip = 0;
            var take = 9999; // Get all order

            var orderPage = await _orderWebApiClient.GetOrders(_setting.AccessToken, null, null, null, skip, take);

            var orders = new OrderDto();
            if (orderPage.Items?.Any() ?? false)
            {
                foreach (var item in orderPage.Items)
                {
                    orders.Orders.Add(OrderInfoDto.FromOrderList(item));
                }
            }

            return orders;
        }

        public async Task<IList<OrderInstoreDto>> GetOrdersInstore(int memberinternalkey, int limit, int detail)
        {
            var skip = 0;
            var take = limit;

            var orderPage = await _orderWebApiClient.GetOrdersInstore(_setting.AccessToken, null, skip, take);

            var orders = new List<OrderInstoreDto>();
            if (orderPage.Items?.Any() ?? false)
            {
                foreach (var item in orderPage.Items)
                {
                    orders.Add(OrderInstoreDto.FromOrderList(item, memberinternalkey.ToString()));
                }
            }

            return orders;
        }

        public async Task<OrderInstoreDto> GetOrderInstoreById(string referenceId)
        {
            var order = await _orderWebApiClient.GetOrderInstoreByreference(_setting.AccessToken, referenceId);
            var result = OrderInstoreDto.FromOrder(order);
            if (double.TryParse(RemoveSpecialChars(order?.Summary?.TaxTotal), out double tax))
            {
                result.TotalTax = tax;
            }
            if (double.TryParse(RemoveSpecialChars(order?.Summary?.Total), out double totalAmount))
            {
                result.TotalAmount = totalAmount;
            }

            return result;
        }

        public async Task<bool> PlaceOrder(string referenceId, string storeId, string cartVersion)
        {
            var result = await _orderWebApiClient.PlaceOrder(_setting.AccessToken, storeId, cartVersion);

            // Send order info to Amazon SQS
            _ = Task.Run(() => _mediator.Publish(new SendOrderInfoEvent
            {
                AccessToken = _setting.AccessToken,
                SQSQueueUrl = _sqsSetting.QueueUrl,
                OrderReferenceId = referenceId
            }));

            return result;
        }
    }
}
