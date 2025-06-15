namespace RestaurantReservationSystem.Domain.Models
{
    public class RestaurantModel
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OpeningHours { get; set; }
    }
}