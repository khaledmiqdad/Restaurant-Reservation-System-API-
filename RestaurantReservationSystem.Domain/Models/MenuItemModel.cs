namespace RestaurantReservationSystem.Domain.Models
{
    public class MenuItemModel
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int RestaurantId { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }
}