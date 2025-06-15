using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class OrderSeeder : IDataSeeder<Order>
{
    private readonly List<Reservation> _reservations;
    private readonly List<Employee> _employees;

    public OrderSeeder(List<Reservation> reservations, List<Employee> employees)
    {
        _reservations = reservations;
        _employees = employees;
    }
    public async Task<List<Order>> SeedAsync(RestaurantReservationDbContext context)
    {
        var orders = new List<Order>
        {
            new() { ReservationId = _reservations[0].ReservationId, EmployeeId = _employees[0].EmployeeId, OrderDate = DateTime.Now.AddHours(-1), TotalAmount = 25.5m },
            new() { ReservationId = _reservations[1].ReservationId, EmployeeId = _employees[1].EmployeeId, OrderDate = DateTime.Now.AddHours(-2), TotalAmount = 40m },
            new() { ReservationId = _reservations[2].ReservationId, EmployeeId = _employees[2].EmployeeId, OrderDate = DateTime.Now.AddHours(-3), TotalAmount = 55m },
            new() { ReservationId = _reservations[3].ReservationId, EmployeeId = _employees[3].EmployeeId, OrderDate = DateTime.Now.AddHours(-4), TotalAmount = 30m },
            new() { ReservationId = _reservations[4].ReservationId, EmployeeId = _employees[4].EmployeeId, OrderDate = DateTime.Now.AddHours(-5), TotalAmount = 65m }
        };

        await context.Orders.AddRangeAsync(orders);
        await context.SaveChangesAsync();

        return orders;
    }
}