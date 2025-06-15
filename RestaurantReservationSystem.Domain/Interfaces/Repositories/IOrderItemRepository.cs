using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IOrderItemRepository
{
    Task<List<OrderItemModel>> GetAllAsync();
    Task<OrderItemModel> GetByIdAsync(int id);
    Task AddAsync(OrderItemModel orderItem);
    Task UpdateAsync(OrderItemModel orderItem);
    Task DeleteAsync(int id);
    Task<List<OrderItemModel>> GetOrderItemsByOrderIdAsync(int orderId);
    Task<List<OrderItemModel>> GetOrderItemsByMenuItemIdAsync(int menuItemId);
}