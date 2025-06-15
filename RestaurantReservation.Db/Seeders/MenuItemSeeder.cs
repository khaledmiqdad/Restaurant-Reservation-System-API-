using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class MenuItemSeeder : IDataSeeder<MenuItem>
{
    private readonly List<Restaurant> _restaurants;

    public MenuItemSeeder(List<Restaurant> restaurants)
    {
        _restaurants = restaurants;
    }

    public async Task<List<MenuItem>> SeedAsync(RestaurantReservationDbContext context)
    {
        var menuItems = new List<MenuItem>
        {
            new() { Name = "Burger", Description = "Beef with cheese", Price = 10, RestaurantId = _restaurants[0].RestaurantId },
            new() { Name = "Pasta", Description = "White sauce", Price = 12, RestaurantId = _restaurants[1].RestaurantId },
            new() { Name = "Salad", Description = "Greek salad", Price = 8, RestaurantId = _restaurants[2].RestaurantId },
            new() { Name = "Steak", Description = "Ribeye", Price = 22, RestaurantId = _restaurants[3].RestaurantId },
            new() { Name = "Sushi", Description = "Mixed roll", Price = 18, RestaurantId = _restaurants[4].RestaurantId }
        };

        await context.MenuItems.AddRangeAsync(menuItems);
        await context.SaveChangesAsync();

        return menuItems;
    }
}