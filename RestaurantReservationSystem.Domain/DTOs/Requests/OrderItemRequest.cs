namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update an item in an order.
    /// </summary>
    public class OrderItemRequest
    {
        /// <summary>
        /// The unique identifier of the order to which this item belongs.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The unique identifier of the menu item being ordered.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the menu item in the order.
        /// </summary>
        public int Quantity { get; set; }
    }
}