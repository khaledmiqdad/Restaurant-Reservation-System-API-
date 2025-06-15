using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing customer-related operations.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieves a list of all customer.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of customer response DTOs.</returns>
        Task<List<CustomerResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the customer response DTO if found; otherwise, null.</returns>
        Task<CustomerResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new customer to the system.
        /// </summary>
        /// <param name="request">The request DTO containing customer details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<CustomerResponse> CreateAsync(CustomerRequest request);

        /// <summary>
        /// Updates the details of an existing customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to update.</param>
        /// <param name="request">The request DTO containing updated customer information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<CustomerResponse?> UpdateAsync(int id, CustomerRequest request);

        /// <summary>
        /// Deletes an customer from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves the customer associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer response DTO if found; otherwise, null.</returns>
        Task<CustomerResponse?> GetCustomerByReservationIdAsync(int reservationId);
    }
}