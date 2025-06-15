namespace RestaurantReservationSystem.Domain.DTOs.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
    }
}