using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class ReservationSeeder : IDataSeeder<Reservation>
{
    private readonly List<Restaurant> _restaurants;
    private readonly List<Customer> _customers;
    private readonly List<Table> _table;

    public ReservationSeeder(List<Customer> customers, List<Restaurant> restaurants, List<Table> table)
    {
        _restaurants = restaurants;
        _customers = customers;
        _table = table;
    }
    public async Task<List<Reservation>> SeedAsync(RestaurantReservationDbContext context)
    {
        var reservations = new List<Reservation>
        {
            new() { CustomerId = _customers[0].CustomerId, RestaurantId = _restaurants[0].RestaurantId, TableId = _table[0].TableId, ReservationDate = DateTime.Today.AddDays(1), PartySize = 2 },
            new() { CustomerId = _customers[1].CustomerId, RestaurantId = _restaurants[1].RestaurantId, TableId = _table[1].TableId, ReservationDate = DateTime.Today.AddDays(2), PartySize = 3 },
            new() { CustomerId = _customers[2].CustomerId, RestaurantId = _restaurants[2].RestaurantId, TableId = _table[2].TableId, ReservationDate = DateTime.Today.AddDays(3), PartySize = 4 },
            new() { CustomerId = _customers[3].CustomerId, RestaurantId = _restaurants[3].RestaurantId, TableId = _table[3].TableId, ReservationDate = DateTime.Today.AddDays(4), PartySize = 2 },
            new() { CustomerId = _customers[4].CustomerId, RestaurantId = _restaurants[4].RestaurantId, TableId = _table[4].TableId, ReservationDate = DateTime.Today.AddDays(5), PartySize = 5 }
        };

        await context.Reservations.AddRangeAsync(reservations);
        await context.SaveChangesAsync();

        return reservations;
    }
}