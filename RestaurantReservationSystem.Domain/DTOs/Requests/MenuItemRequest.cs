namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update a menu item for a restaurant.
    /// </summary>
    public class MenuItemRequest
    {
        /// <summary>
        /// The unique identifier of the restaurant to which the menu item belongs.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The name of the menu item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An optional description of the menu item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// The price of the menu item.
        /// </summary>
        public decimal Price { get; set; }
    }
}