namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for a reservation.
    /// </summary>
    public class ReservationResponse
    {
        /// <summary>
        /// The unique identifier of the reservation.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The unique identifier of the customer who made the reservation.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The unique identifier of the restaurant for the reservation.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The unique identifier of the reserved table.
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// The date and time of the reservation.
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// The number of people included in the reservation.
        /// </summary>
        public int PartySize { get; set; }
    }
}