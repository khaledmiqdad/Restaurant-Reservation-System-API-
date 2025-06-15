using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing table-related operations.
    /// </summary>
    public interface ITableService
    {
        /// <summary>
        /// Retrieves a list of all tables.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. 
        /// The task result contains a list of table response DTOs.
        /// </returns>
        Task<List<TableResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific table by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the table.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the table response DTO if found; otherwise, null.
        /// </returns>
        Task<TableResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new table in the system.
        /// </summary>
        /// <param name="request">The request DTO containing table details.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the created table response DTO.
        /// </returns>
        Task<TableResponse> CreateAsync(TableRequest request);

        /// <summary>
        /// Updates the details of an existing table.
        /// </summary>
        /// <param name="id">The unique identifier of the table to update.</param>
        /// <param name="request">The request DTO containing updated table information.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains the updated table response DTO if found; otherwise, null.
        /// </returns>
        Task<TableResponse?> UpdateAsync(int id, TableRequest request);

        /// <summary>
        /// Deletes a table from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the table to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(int id);

        Task<List<TableResponse>> GetTablesByRestaurantIdAsync(int restaurantId);

        /// <summary>
        /// Retrieves the table associated with a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the table response DTO if found; otherwise, null.</returns>
        Task<TableResponse?> GetTableByReservationIdAsync(int reservationId);
    }
}