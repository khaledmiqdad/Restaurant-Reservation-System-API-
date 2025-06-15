using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

namespace RestaurantReservation.Db.Repositories.ReportRepositories;

internal class CustomerReportRepository : ICustomerReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public CustomerReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerView>> GetCustomersByPartySizeAsync(int minPartySize)
    {
        var param = new SqlParameter("@minPartySize", minPartySize);
        return await _context
            .CustomerView
            .FromSqlRaw("EXEC GetCustomersByPartySize @minPartySize", param)
            .AsNoTracking()
            .ToListAsync();
    }
}
