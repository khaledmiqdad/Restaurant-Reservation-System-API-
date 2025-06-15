using RestaurantReservation.Db.Entities;

namespace RestaurantReservation.Db.Seeders;

internal class OrderItemSeeder : IDataSeeder<OrderItem>
{
    private readonly List<Order> _orders;
    private readonly List<MenuItem> _menuItems;

    public OrderItemSeeder(List<Order> Orders, List<MenuItem> menuItems)
    {
        _orders = Orders;
        _menuItems = menuItems;
    }
    public async Task<List<OrderItem>> SeedAsync(RestaurantReservationDbContext context)
    {
        var orderItems = new List<OrderItem>
        {
            new() { OrderId = _orders[0].OrderId, ItemId = _menuItems[0].ItemId, Quantity = 2 },
            new() { OrderId = _orders[1].OrderId, ItemId = _menuItems[1].ItemId, Quantity = 1 },
            new() { OrderId = _orders[2].OrderId, ItemId = _menuItems[2].ItemId, Quantity = 3 },
            new() { OrderId = _orders[3].OrderId, ItemId = _menuItems[3].ItemId, Quantity = 5 },
            new() { OrderId = _orders[4].OrderId, ItemId = _menuItems[4].ItemId, Quantity = 2 }
        };

        await context.OrderItems.AddRangeAsync(orderItems);
        await context.SaveChangesAsync();
        return orderItems;
    }
}