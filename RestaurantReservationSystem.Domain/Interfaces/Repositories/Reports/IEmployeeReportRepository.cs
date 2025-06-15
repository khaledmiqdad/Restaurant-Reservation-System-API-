namespace RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

public interface IEmployeeReportRepository
{
    Task<List<EmployeeRestaurantDetails>> GetEmployeesAsync();
}
