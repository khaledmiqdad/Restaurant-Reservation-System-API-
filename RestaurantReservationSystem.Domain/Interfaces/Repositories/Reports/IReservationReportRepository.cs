namespace RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

public interface IReservationReportRepository
{
    Task<List<ReservationDetailsView>> GetReservationsAsync();
}
