namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update a customer.
    /// </summary>
    public class CustomerRequest
    {
        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the customer's email address. Optional.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the customer's phone number. Optional.
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}