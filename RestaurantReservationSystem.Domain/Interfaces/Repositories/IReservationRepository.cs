using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<List<ReservationModel>> GetAllAsync();
    Task<ReservationModel> GetByIdAsync(int id);
    Task AddAsync(ReservationModel reservation);
    Task UpdateAsync(ReservationModel reservation);
    Task DeleteAsync(int id);
    Task<List<ReservationModel>> GetReservationsByRestaurantIdAsync(int restaurantId);
    Task<List<ReservationModel>> GetReservationsByTableIdAsync(int tableId);
    Task<ReservationModel?> GetReservationByOrderIdAsync(int orderId);
    Task<ReservationModel?> GetReservationByIdWithOrdersAsync(int id);
    Task<List<ReservationModel>> GetReservationsByCustomerIdAsync(int customerId);
}