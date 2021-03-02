using DXP.SmartConnect.Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderInfoDto
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string DatePlaced { get; set; }
        public string OrderTotal { get; set; }
        public string FulfillmentType { get; set; }
        public string FulfillmentDateTime { get; set; }
        public IList<OrderItemDto> Items { get; set; }
        public string PointsSavingsTotal { get; set; }
        public string PointsUsedTotal { get; set; }
        public object Invoice { get; set; }

        /// <summary>
        /// Transform object from type OrderList to type OrderInfoDto
        /// </summary>
        /// <returns>OrderInfoDto</returns>
        public static OrderInfoDto FromOrderList(OrderList order)
        {
            var orderDto = new OrderInfoDto
            {
                OrderId = order.OrderRef,
                Status = order.Status,
                DatePlaced = order.DatePlaced,
                OrderTotal = order.EstTotal,
                FulfillmentType = order.FulfilmentType,
                FulfillmentDateTime = order.FulfilmentDateTime,
                Items = new List<OrderItemDto>()
            };

            if (order.LineItems?.Any() ?? false)
            {
                foreach (var item in order.LineItems)
                {
                    orderDto.Items.Add(OrderItemDto.FromOrderListItem(item));
                }
            }

            return orderDto;
        }

        /// <summary>
        /// Transform object from type OrderList to type OrderInfoDto
        /// </summary>
        /// <returns>OrderInfoDto</returns>
        public static OrderInfoDto FromOrder(Order order)
        {
            var orderDto = new OrderInfoDto
            {
                OrderId = order.OrderReference,
                Status = order.Status,
                OrderTotal = order.Summary?.OrderValue,
                FulfillmentType = order.Fulfilment?.FulfilmentType,
                Items = new List<OrderItemDto>()
            };

            if (order.LineItems?.Any() ?? false)
            {
                foreach (var item in order.LineItems)
                {
                    orderDto.Items.Add(OrderItemDto.FromOrderItem(item));
                }
            }

            return orderDto;
        }
    }
}