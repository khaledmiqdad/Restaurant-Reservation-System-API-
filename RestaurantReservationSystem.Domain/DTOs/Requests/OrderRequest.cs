namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update an order.
    /// </summary>
    public class OrderRequest
    {
        /// <summary>
        /// The unique identifier of the reservation associated with the order.
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// The unique identifier of the employee who handled the order.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The date and time when the order was placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// The total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}