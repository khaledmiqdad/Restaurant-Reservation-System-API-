namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for a table in a restaurant.
    /// </summary>
    public class TableResponse
    {
        /// <summary>
        /// The unique identifier of the table.
        /// </summary>
        public int TableId { get; set; }

        /// <summary>
        /// The unique identifier of the restaurant to which the table belongs.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The seating capacity of the table.
        /// </summary>
        public int Capacity { get; set; }
    }
}