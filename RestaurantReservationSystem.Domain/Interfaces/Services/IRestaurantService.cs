using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for restaurant-related operations.
    /// </summary>
    public interface IRestaurantService
    {
        /// <summary>
        /// Retrieves all restaurants.
        /// </summary>
        /// <returns>A collection of restaurant response DTOs.</returns>
        Task<List<RestaurantResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a restaurant by its ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant.</param>
        /// <returns>The restaurant response DTO.</returns>
        Task<RestaurantResponse> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The restaurant request DTO containing creation data.</param>
        /// <returns>The newly created restaurant response DTO.</returns>
        Task<RestaurantResponse> CreateAsync(RestaurantRequest request);

        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="request">The restaurant request DTO with updated data.</param>
        /// <returns>The updated restaurant response DTO, or null if not found.</returns>
        Task<RestaurantResponse?> UpdateAsync(int id, RestaurantRequest request);

        /// <summary>
        /// Deletes a restaurant by its ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete.</param>
        /// <returns>True if the restaurant was deleted successfully; otherwise, false.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves the restaurant associated with a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantByEmployeeIdAsync(int employeeId);

        /// <summary>
        /// Retrieves all reservations handled by a specific table.
        /// </summary>
        /// <param name="tableId">The unique identifier of the table.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of reservation response DTOs.
        /// </returns>
        Task<RestaurantResponse?> GetRestaurantByTableIdAsync(int tableId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific menuItem.
        /// </summary>
        /// <param name="menuItemId">The unique identifier of the menuItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantByMenuItamIdAsync(int menuItemId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<RestaurantResponse?> GetRestaurantByReservationIdAsync(int reservationId);
    }
}