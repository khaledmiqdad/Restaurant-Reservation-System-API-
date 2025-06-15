namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for a restaurant.
    /// </summary>
    public class RestaurantResponse
    {
        /// <summary>
        /// The unique identifier of the restaurant.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The name of the restaurant.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The full address of the restaurant.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The contact phone number for the restaurant.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The opening hours of the restaurant (e.g., "9 AM - 10 PM").
        /// </summary>
        public string OpeningHours { get; set; }
    }
}