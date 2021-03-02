using DXP.SmartConnect.Ecom.Core.Entities;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderInstoreItemDto
    {
        public string Dept { get; set; }
        public string ItemUPC { get; set; }
        public string LinId { get; set; }
        public string ItemDescription { get; set; }
        public double? Price { get; set; }
        public int? Qty { get; set; }

        /// <summary>
        /// Transform object from type OrderItem to type OrderItemDto
        /// </summary>
        /// <returns>OrderItemDto</returns>
        public static OrderInstoreItemDto FromOrderItem(OrderItem item)
        {
            var orderItem = new OrderInstoreItemDto
            {
                Dept = item.Category,
                ItemUPC = item.Sku,
                LinId = $"sku:{item.Sku}",
                ItemDescription = item.Name,
            };

            if (int.TryParse(item.Quantity, out int quantity))
            {
                orderItem.Qty = quantity;
            }

            return orderItem;
        }

        /// <summary>
        /// Transform object from type OrderListItem to type OrderItemDto
        /// </summary>
        /// <returns>OrderItemDto</returns>
        public static OrderInstoreItemDto FromOrderListItem(OrderListItem item)
        {
            var orderItem = new OrderInstoreItemDto
            {
                ItemUPC = item.Sku,
                LinId = $"sku:{item.Sku}",
            };

            if (int.TryParse(item.Quantity, out int quantity))
            {
                orderItem.Qty = quantity;
            }

            return orderItem;
        }
    }
}