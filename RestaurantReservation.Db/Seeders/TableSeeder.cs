using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class TableSeeder : IDataSeeder<Table>
{

    private readonly List<Restaurant> _restaurants;

    public TableSeeder(List<Restaurant> restaurants)
    {
        _restaurants = restaurants;
    }
    public async Task<List<Table>> SeedAsync(RestaurantReservationDbContext context)
    {
        var tables = new List<Table>();

            tables.AddRange(new[]
            {
                new Table { RestaurantId = _restaurants[0].RestaurantId, Capacity = 2 },
                new Table { RestaurantId = _restaurants[1].RestaurantId, Capacity = 4 },
                new Table { RestaurantId = _restaurants[2].RestaurantId, Capacity = 6 },
                new Table { RestaurantId = _restaurants[3].RestaurantId, Capacity = 7 },
                new Table { RestaurantId = _restaurants[4].RestaurantId, Capacity = 3 }
            });

        await context.Tables.AddRangeAsync(tables);
        await context.SaveChangesAsync();

        return tables;
    }
}
