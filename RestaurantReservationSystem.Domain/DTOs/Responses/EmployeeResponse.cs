namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    /// <summary>
    /// Represents the response data returned for an employee.
    /// </summary>
    public class EmployeeResponse
    {
        /// <summary>
        /// The unique identifier of the employee.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The identifier of the restaurant the employee works at.
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