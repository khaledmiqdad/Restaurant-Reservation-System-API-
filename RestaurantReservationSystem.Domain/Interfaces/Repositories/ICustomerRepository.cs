using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<List<CustomerModel>> GetAllAsync();
    Task<CustomerModel> GetByIdAsync(int id);
    Task AddAsync(CustomerModel customer);
    Task UpdateAsync(CustomerModel customer);
    Task DeleteAsync(int id);
    Task<CustomerModel?> GetCustomerByReservationIdAsync(int reservationId);
}