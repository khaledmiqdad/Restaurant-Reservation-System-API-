namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update a restaurant.
    /// </summary>
    public class RestaurantRequest
    {
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