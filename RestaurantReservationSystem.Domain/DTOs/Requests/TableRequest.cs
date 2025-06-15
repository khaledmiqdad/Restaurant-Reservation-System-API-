namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update a table in a restaurant.
    /// </summary>
    public class TableRequest
    {
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