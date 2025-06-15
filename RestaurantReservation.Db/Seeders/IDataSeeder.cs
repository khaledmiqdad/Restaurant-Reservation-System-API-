namespace RestaurantReservation.Db.Seeders;

public interface IDataSeeder<T>
{
    Task<List<T>> SeedAsync(RestaurantReservationDbContext context);
}