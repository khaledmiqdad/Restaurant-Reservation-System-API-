namespace RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

public interface IRevenueReportRepository
{
    Task<decimal> GetTotalRevenueByRestaurantAsync(int restaurantId);
}
