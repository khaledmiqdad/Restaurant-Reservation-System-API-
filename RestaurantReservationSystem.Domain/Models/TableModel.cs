namespace RestaurantReservationSystem.Domain.Models
{
    public class TableModel
    {
        public int TableId { get; set; }
        public int RestaurantId { get; set; }
        public int Capacity { get; set; }
    }
}