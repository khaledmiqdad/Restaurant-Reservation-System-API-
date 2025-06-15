
using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;
namespace RestaurantReservation.Db.Repositories.ReportRepositories;

internal class ReservationReportRepository : IReservationReportRepository
{
    private readonly RestaurantReservationDbContext _context;

    public ReservationReportRepository(RestaurantReservationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReservationDetailsView>> GetReservationsAsync()
    {
        return await _context.Set<ReservationDetailsView>().ToListAsync();
    }
}
