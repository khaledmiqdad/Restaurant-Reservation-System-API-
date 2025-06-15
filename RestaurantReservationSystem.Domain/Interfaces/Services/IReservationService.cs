using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing reservation-related operations.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Retrieves a list of all reservation.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of reservation response DTOs.</returns>
        Task<List<ReservationResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific reservation by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the reservation response DTO if found; otherwise, null.</returns>
        Task<ReservationResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new reservation to the system.
        /// </summary>
        /// <param name="request">The request DTO containing reservation details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<ReservationResponse> CreateAsync(ReservationRequest request);

        /// <summary>
        /// Updates the details of an existing reservation.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation to update.</param>
        /// <param name="request">The request DTO containing updated reservation information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<ReservationResponse?> UpdateAsync(int id, ReservationRequest request);

        /// <summary>
        /// Deletes an reservation from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves a list of all Reservation handled by a specific Customer.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result contains a list of Reservation response DTOs.
        /// </returns>
        Task<List<ReservationResponse>> GetReservationsByCustomerIdAsync(int customerId);

        /// <summary>
        /// Retrieves the reservation associated with a given order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>The associated <see cref="ReservationResponse"/> if found; otherwise, null.</returns>
        Task<ReservationResponse?> GetReservationByOrderIdAsync(int orderId);

        /// <summary>
        /// Retrieves all reservations made for a specific restaurant.
        /// </summary>
        /// <param name="restaurantId">The unique identifier of the restaurant.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of 
        /// <see cref="ReservationResponse"/> objects representing the reservations for the specified restaurant.
        /// </returns>
        Task<List<ReservationResponse>> GetReservationsByRestaurantIdAsync(int restaurantId);

        Task<List<ReservationResponse>> GetReservationsByTableIdAsync(int tableId);
    }
}