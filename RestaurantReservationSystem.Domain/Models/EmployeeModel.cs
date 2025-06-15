namespace RestaurantReservationSystem.Domain.Models;

public class EmployeeModel
{
    public int EmployeeId { get; set; }
    public int RestaurantId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Position { get; set; }
    public List<OrderModel> Orders { get; set; }
}