using DXP.SmartConnect.Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderInstoreDto
    {
        public string Id { get; set; }
        public string Cardid { get; set; }

        public string StoreNum { get; set; }
        public string TransActionType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double? TotalAmount { get; set; }
        public double? TotalTax { get; set; }
        public int? TotalItems { get; set; }
        public IList<OrderInstoreItemDto> PurchaseDetails { get; set; }

        //v2 dto
        public DateTime Createddate { get; set; }
        public int? Memberinternalkey { get; set; }
        public int? Buyingunitinternalkey { get; set; }
        public int? Tillid { get; set; }
        public double? Totalamount { get; set; }
        public double? Tax { get; set; }
        public string Store { get; set; }
        public OrderInstoreUpcDto PurchaseDetailItems { get; set; }

        /// <summary>
        /// Transform object from type OrderList to type OrderInstoreDto (v2)
        /// </summary>
        /// <returns>OrderInstoreDto</returns>
        public static OrderInstoreDto FromOrderList(OrderList orderList, string cardId)
        {
            var orderDto = new OrderInstoreDto
            {
                Id = orderList.OrderRef,
                Cardid = cardId,
                Store = orderList.RetailerStoreId,
                PurchaseDetailItems = new OrderInstoreUpcDto()
            };

            if (orderList.LineItems?.Any() ?? false)
            {
                foreach (var item in orderList.LineItems)
                {
                    orderDto.PurchaseDetailItems.UpcList.Add(item.Sku);
                }
            }

            return orderDto;
        }

        /// <summary>
        /// Transform object from type Order to type OrderInstoreDto
        /// </summary>
        /// <returns>OrderInstoreDto</returns>
        public static OrderInstoreDto FromOrder(Order order)
        {
            var orderDto = new OrderInstoreDto
            {
                Id = order.OrderReference,
                StoreNum = order.Store?.RetailerStoreId,
                TotalItems = order.ItemCount,
                TransActionType = order.Fulfilment?.FulfilmentType,
                PurchaseDetails = new List<OrderInstoreItemDto>()
            };

            if (order.LineItems?.Any() ?? false)
            {
                foreach (var item in order.LineItems)
                {
                    orderDto.PurchaseDetails.Add(OrderInstoreItemDto.FromOrderItem(item));
                }
            }

            return orderDto;
        }
    }
}
