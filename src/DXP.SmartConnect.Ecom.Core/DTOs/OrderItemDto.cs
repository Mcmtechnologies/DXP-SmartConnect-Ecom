using DXP.SmartConnect.Ecom.Core.Entities;

namespace DXP.SmartConnect.Ecom.Core.DTOs
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public string Sku { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string ItemType { get; set; }
        public string Note { get; set; }
        public string CurrentPrice { get; set; }
        public object CurrentUnitPrice { get; set; }
        public object RegularPrice { get; set; }
        public string LineTotal { get; set; }
        public bool InStock { get; set; }
        public bool IsAvailable { get; set; }
        public string ItemKey { get; set; }
        public string QuantityRedeemedWithPoints { get; set; }
        public string NumberOfPoints { get; set; }


        /// <summary>
        /// Transform object from type OrderListItem to type OrderItemDto
        /// </summary>
        /// <returns>OrderItemDto</returns>
        public static OrderItemDto FromOrderListItem(OrderListItem item)
        {
            var orderItem = new OrderItemDto
            {
                Id = item.Sku,
                Sku = item.Sku,
                Note = item.Note
            };

            if (int.TryParse(item.Quantity, out int quantity))
            {
                orderItem.Quantity = quantity;
            }

            return orderItem;
        }

        /// <summary>
        /// Transform object from type OrderItem to type OrderItemDto
        /// </summary>
        /// <returns>OrderItemDto</returns>
        public static OrderItemDto FromOrderItem(OrderItem item)
        {
            var orderItem = new OrderItemDto
            {
                Id = item.Sku,
                Sku = item.Sku,
                Brand = item.Brand,
                Name = item.Name,
                CurrentPrice = item.Price,
                Note = item.Note,
                CurrentUnitPrice = item.Price,
                RegularPrice = item.TotalPrice,
                LineTotal = item.LineItemPrice,
                ItemKey = $"sku:{item.Sku}",
            };

            if (int.TryParse(item.Quantity, out int quantity))
            {
                orderItem.Quantity = quantity;
            }

            return orderItem;
        }
    }
}