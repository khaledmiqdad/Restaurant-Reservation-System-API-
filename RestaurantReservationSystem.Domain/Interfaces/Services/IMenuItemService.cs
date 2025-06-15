using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing menuItem-related operations.
    /// </summary>
    public interface IMenuItemService
    {
        /// <summary>
        /// Retrieves a list of all menuItem.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of menuItem response DTOs.</returns>
        Task<List<MenuItemResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific menuItem by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the menuItem response DTO if found; otherwise, null.</returns>
        Task<MenuItemResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new menuItem to the system.
        /// </summary>
        /// <param name="request">The request DTO containing menuItem details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<MenuItemResponse> CreateAsync(MenuItemRequest request);

        /// <summary>
        /// Updates the details of an existing menuItem.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem to update.</param>
        /// <param name="request">The request DTO containing updated menuItem information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<MenuItemResponse?> UpdateAsync(int id, MenuItemRequest request);

        /// <summary>
        /// Deletes an menuItem from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the menuItem to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all Ordered Menu Items handled by a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of menuItem response DTOs.</returns>
        Task<List<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId);

        /// <summary>
        /// Retrieves a list of employees who work at the specified restaurant.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant to get employees for.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of employee response DTOs.</returns>

        Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific order.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<MenuItemResponse?> GetMenuItemByOrderItemIdAsync(int orderItemId);
    }
}