using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class CustomerSeeder : IDataSeeder<Customer>
{
    public async Task<List<Customer>> SeedAsync(RestaurantReservationDbContext context)
    {
        var customers = new List<Customer>
        {
            new() { FirstName = "Mohamed", LastName = "Fawzy", Email = "m.fawzy@mail.com", PhoneNumber = "0101111222" },
            new() { FirstName = "Layla", LastName = "Ali", Email = "layla.ali@mail.com", PhoneNumber = "0103333444" },
            new() { FirstName = "Ahmed", LastName = "Zaki", Email = "ahmed.zaki@mail.com", PhoneNumber = "0105555666" },
            new() { FirstName = "Dina", LastName = "Kareem", Email = "dina.kareem@mail.com", PhoneNumber = "0107777888" },
            new() { FirstName = "Samir", LastName = "Farid", Email = "samir.farid@mail.com", PhoneNumber = "0109999000" }
        };

        await context.Customers.AddRangeAsync(customers);
        await context.SaveChangesAsync();

        return customers;
    }
}