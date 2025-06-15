using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class EmployeeSeeder : IDataSeeder<Employee>
{
    private readonly List<Restaurant> _restaurants;

    public EmployeeSeeder(List<Restaurant> restaurants)
    {
        _restaurants = restaurants;
    }
    public async Task<List<Employee>> SeedAsync(RestaurantReservationDbContext context)
    {
        var employees = new List<Employee>
        {
            new() { FirstName = "Ali", LastName = "Hassan", Position = "Manager", RestaurantId = _restaurants[0].RestaurantId },
            new() { FirstName = "Sara", LastName = "Kamal", Position = "Chef", RestaurantId = _restaurants[1].RestaurantId },
            new() { FirstName = "Omar", LastName = "Salem", Position = "Waiter", RestaurantId = _restaurants[2].RestaurantId },
            new() { FirstName = "Nora", LastName = "Yousef", Position = "Manager", RestaurantId = _restaurants[3].RestaurantId },
            new() { FirstName = "Khaled", LastName = "Fathy", Position = "Waiter", RestaurantId = _restaurants[4].RestaurantId },
        };

        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();

        return employees;
    }
}