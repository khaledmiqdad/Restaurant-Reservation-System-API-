namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update a reservation.
    /// </summary>
    public class ReservationRequest
    {
        /// <summary>
        /// The unique identifier of the customer making the reservation.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The unique identifier of the restaurant where the reservation is made.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The unique identifier of the table reserved.
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