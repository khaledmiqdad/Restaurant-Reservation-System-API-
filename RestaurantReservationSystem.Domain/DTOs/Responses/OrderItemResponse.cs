namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data for an individual item within an order.
    /// </summary>
    public class OrderItemResponse
    {
        /// <summary>
        /// The unique identifier of the order item.
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// The identifier of the order that this item belongs to.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The identifier of the menu item included in the order.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the menu item ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The details of the menu item associated with this order item.
        /// </summary>
        public MenuItemResponse MenuItem { get; set; }

    }
}