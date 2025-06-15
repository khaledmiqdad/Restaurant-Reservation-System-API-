namespace RestaurantReservationSystem.Domain.DTOs.Requests
{
    /// <summary>
    /// Represents the data required to create or update an employee.
    /// </summary>
    public class EmployeeRequest
    {
        /// <summary>
        /// The identifier of the restaurant the employee belongs to.
        /// </summary>
        public int RestaurantId { get; set; }

        /// <summary>
        /// The first name of the employee.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the employee.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The job title or position of the employee.
        /// </summary>
        public string Position { get; set; }
    }
}