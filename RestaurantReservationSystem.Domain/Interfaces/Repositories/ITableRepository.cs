using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface ITableRepository
{
    Task<List<TableModel>> GetAllAsync();
    Task<TableModel> GetByIdAsync(int id);
    Task AddAsync(TableModel table);
    Task UpdateAsync(TableModel table);
    Task DeleteAsync(int id);
    Task<List<TableModel>> GetByRestaurantIdAsync(int restaurantId);
    Task<TableModel?> GetTableByReservationIdAsync(int reservationId);
}