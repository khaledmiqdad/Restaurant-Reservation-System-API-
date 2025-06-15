using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing employee-related operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Retrieves a list of all Managers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of managers employee response DTOs.
        /// </returns>

        Task<List<EmployeeResponse>> ListManagersAsync();

        /// <summary>
        /// Retrieves a list of all employees.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of employee response DTOs.</returns>
        Task<List<EmployeeResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the employee response DTO if found; otherwise, null.</returns>
        Task<EmployeeResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new employee to the system.
        /// </summary>
        /// <param name="request">The request DTO containing employee details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<EmployeeResponse> CreateAsync(EmployeeRequest request);

        /// <summary>
        /// Updates the details of an existing employee.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to update.</param>
        /// <param name="request">The request DTO containing updated employee information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<EmployeeResponse?> UpdateAsync(int id, EmployeeRequest request);

        /// <summary>
        /// Deletes an employee from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        Task<List<EmployeeResponse>> GetEmployeesByRestaurantIdAsync(int restaurantId);

        /// <summary>
        /// Retrieves the employee who processed a given order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The associated <see cref="EmployeeResponse"/> if found; otherwise, null.</returns>
        Task<EmployeeResponse?> GetEmployeeByOrderIdAsync(int orderId);
    }
}