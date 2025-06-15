using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;
public interface IRestaurantRepository
{
    Task<List<RestaurantModel>> GetAllAsync();
    Task<RestaurantModel> GetByIdAsync(int id);
    Task AddAsync(RestaurantModel restaurant);
    Task UpdateAsync(RestaurantModel restaurant);
    Task DeleteAsync(int id);
    Task<RestaurantModel?> GetRestaurantByEmployeeIdAsync(int employeeId);
    Task<RestaurantModel?> GetRestaurantByTableIdAsync(int tableId);
    Task<RestaurantModel?> GetRestaurantByMenuItemIdAsync(int menuItemId);
    Task<RestaurantModel?> GetRestaurantByReservationIdAsync(int reservationId);
}