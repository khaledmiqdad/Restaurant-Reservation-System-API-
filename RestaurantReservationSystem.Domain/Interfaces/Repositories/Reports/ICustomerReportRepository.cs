namespace RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

public interface ICustomerReportRepository
{
    Task<List<CustomerView>> GetCustomersByPartySizeAsync(int minPartySize);
}