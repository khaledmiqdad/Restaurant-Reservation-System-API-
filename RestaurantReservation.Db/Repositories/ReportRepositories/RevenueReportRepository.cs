using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

namespace RestaurantReservation.Db.Repositories.ReportRepositories;

internal class RevenueReportRepository : IRevenueReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public RevenueReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetTotalRevenueByRestaurantAsync(int restaurantId)
    {
        var param = new SqlParameter("@restaurantId", restaurantId);

        var result = await _context
            .RevenueResult
            .FromSqlRaw("SELECT dbo.CalculateTotalRevenue(@restaurantId) AS TotalRevenue", param)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return result?.TotalRevenue ?? 0;
    }
}
