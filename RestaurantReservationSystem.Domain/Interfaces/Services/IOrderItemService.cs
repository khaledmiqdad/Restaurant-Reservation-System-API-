using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for managing orderItem-related operations.
    /// </summary>
    public interface IOrderItemService
    {
        /// <summary>
        /// Retrieves a list of all orderItem.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of orderItem response DTOs.</returns>
        Task<List<OrderItemResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific orderItem by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the orderItem response DTO if found; otherwise, null.</returns>
        Task<OrderItemResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new orderItem to the system.
        /// </summary>
        /// <param name="request">The request DTO containing orderItem details.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<OrderItemResponse> CreateAsync(OrderItemRequest request);

        /// <summary>
        /// Updates the details of an existing orderItem.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to update.</param>
        /// <param name="request">The request DTO containing updated orderItem information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<OrderItemResponse?> UpdateAsync(int id, OrderItemRequest request);

        /// <summary>
        /// Deletes an orderItem from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all orderItems handled by a specific orderItems.
        /// </summary>
        /// <param name="menuItemId">The unique identifier of the menuItem</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of orderitem response DTOs.</returns>
        Task<List<OrderItemResponse>> GetOrderItemsByMenuItamIdAsync(int menuItemId);

        /// <summary>
        /// Retrieves all order items handled by a specific order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A collection of <see cref="OrderItemResponse"/> linked to the order.</returns>
        Task<List<OrderItemResponse>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}