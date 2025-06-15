using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class RestaurantSeeder : IDataSeeder<Restaurant>
{
    public async Task<List<Restaurant>> SeedAsync(RestaurantReservationDbContext context)
    {
        if (await context.Restaurants.AnyAsync()) return await context.Restaurants.ToListAsync();

        var restaurants = new List<Restaurant>
        {
            new() { Name = "mm Palace", Address = "Downtown", PhoneNumber = "0123456789", OpeningHours = "08:00 - 23:00" },
            new() { Name = "Ocean View", Address = "Beach Road", PhoneNumber = "0987654321", OpeningHours = "10:00 - 22:00" },
            new() { Name = "Mountain Grill", Address = "Hilltop", PhoneNumber = "0111222333", OpeningHours = "12:00 - 23:00" },
            new() { Name = "City Cafe", Address = "Central Square", PhoneNumber = "0223344556", OpeningHours = "07:00 - 20:00" },
            new() { Name = "Sunset Dine", Address = "Lakeside", PhoneNumber = "0334455667", OpeningHours = "17:00 - 23:59" }
        };
        
        await context.Restaurants.AddRangeAsync(restaurants);
        await context.SaveChangesAsync();

        return restaurants;
    }
}
