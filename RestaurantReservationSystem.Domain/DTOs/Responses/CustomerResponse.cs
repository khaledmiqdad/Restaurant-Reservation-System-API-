namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the data returned for a customer.
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the customer.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer. Optional.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer. Optional.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
