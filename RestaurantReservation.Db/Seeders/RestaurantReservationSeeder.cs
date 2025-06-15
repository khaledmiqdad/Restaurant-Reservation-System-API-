using RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

namespace RestaurantReservation.Db.Seeders;

internal class RestaurantReservationSeeder : IRestaurantReservationSeeder
{
    private readonly RestaurantReservationDbContext _context;

    public RestaurantReservationSeeder(RestaurantReservationDbContext context)
    {
        _context = context;
    }
    public async Task SeedAsync()
    {
        var restaurantSeeder = new RestaurantSeeder();
        var restaurants = await restaurantSeeder.SeedAsync(_context);

        var customerSeeder = new CustomerSeeder();
        var customers = await customerSeeder.SeedAsync(_context);

        var tableSeeder = new TableSeeder(restaurants);
        var tables = await tableSeeder.SeedAsync(_context);

        var employeeSeeder = new EmployeeSeeder(restaurants);
        var employees = await employeeSeeder.SeedAsync(_context);

        var reservationSeeder = new ReservationSeeder(customers, restaurants, tables);
        var reservations = await reservationSeeder.SeedAsync(_context);

        var orderSeeder = new OrderSeeder(reservations, employees);
        var orders = await orderSeeder.SeedAsync(_context);

        var menuItemSeeder = new MenuItemSeeder(restaurants);
        var menuItems = await menuItemSeeder.SeedAsync(_context);

        var orderItemSeeder = new OrderItemSeeder(orders, menuItems);
        await orderItemSeeder.SeedAsync(_context);
    }
}