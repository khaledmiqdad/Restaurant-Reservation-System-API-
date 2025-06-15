namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for a menu item.
    /// </summary>
    public class MenuItemResponse
    {
        /// <summary>
        /// The unique identifier of the menu item.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The identifier of the restaurant that offers the menu item.
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