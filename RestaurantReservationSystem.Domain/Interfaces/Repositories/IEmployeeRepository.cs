using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeModel>> ListManagersAsync();
        Task<List<EmployeeModel>> GetAllAsync();
        Task<EmployeeModel> GetByIdAsync(int id);
        Task AddAsync(EmployeeModel employee);
        Task UpdateAsync(EmployeeModel employee);
        Task DeleteAsync(int id);
        Task<List<EmployeeModel>> GetByRestaurantIdAsync(int restaurantId);
        Task<EmployeeModel?> GetEmployeeByOrderIdAsync(int orderId);
        Task<EmployeeModel?> GetByIdWithOrdersAsync(int id);
    }
}