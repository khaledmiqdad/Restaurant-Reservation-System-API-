using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IMenuItemRepository
{
    Task<List<MenuItemModel>> GetAllAsync();
    Task<MenuItemModel> GetByIdAsync(int id);
    Task AddAsync(MenuItemModel menuItem);
    Task UpdateAsync(MenuItemModel menuItem);
    Task DeleteAsync(int id);
    Task<List<MenuItemModel>> GetMenuItemsByRestaurantIdAsync(int restaurantId);
    Task<List<MenuItemModel>> ListOrderedMenuItemsAsync(int reservationId);
    Task<MenuItemModel?> GetMenuItemByOrderItemIdAsync(int orderItemId);
    Task<MenuItemModel?> GetByIdWithOrderItemsAsync(int id);
}
