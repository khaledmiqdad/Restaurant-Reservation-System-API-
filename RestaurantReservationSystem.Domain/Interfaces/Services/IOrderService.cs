using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines the contract for order-related operations within the Restaurant Reservation System.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Retrieves all orders in the system.
        /// </summary>
        /// <returns>A list of <see cref="OrderResponse"/> representing all orders.</returns>
        Task<List<OrderResponse>> GetAllAsync();

        /// <summary>
        /// Retrieves a specific order by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The corresponding <see cref="OrderResponse"/> if found; otherwise, null.</returns>
        Task<OrderResponse?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new order in the system.
        /// </summary>
        /// <param name="request">The order details provided in <see cref="OrderRequest"/> format.</param>
        Task<OrderResponse?> CreateAsync(OrderRequest request);

        /// <summary>
        /// Updates an existing order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="request">The updated order data.</param>
        Task<OrderResponse?> UpdateAsync(int id, OrderRequest request);

        /// <summary>
        /// Deletes an order by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Retrieves all orders handled by a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of order response DTOs.</returns>
        Task<List<OrderResponse>> GetOrdersByEmployeeIdAsync(int employeeId);

        /// <summary>
        /// Retrieves all orders handled by a specific reservation.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of order response DTOs.</returns>
        Task<List<OrderResponse>> GetOrdersByReservationIdAsync(int reservationId);

        /// <summary>
        /// Retrieves the restaurant associated with a specific order.
        /// </summary>
        /// <param name="orderItemId">The unique identifier of the order.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<OrderResponse?> GetOrderByOrderItemIdAsync(int orderItemId);

        Task<List<OrderResponse>> GetOrdersAndMenuItemsAsync(int reservationId);

        /// <summary>
        /// Retrieves the Average Order Amount associated with a specific employee.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the restaurant response DTO if found; otherwise, null.</returns>
        Task<decimal> GetAverageOrderAmountAsync(int employeeId);
    }
}