namespace RestaurantReservationSystem.Domain.Models
{
    public class OrderItemModel
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public MenuItemModel MenuItem { get; set; }
    }
}