namespace RestaurantReservationSystem.Domain.DTOs.Responses;
/// <summary>
/// Represents the response data returned for an order.
/// </summary>
public class OrderResponse
{
    /// <summary>
    /// The unique identifier of the order.
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// The unique identifier of the reservation associated with the order.
    /// </summary>
    public int ReservationId { get; set; }

    /// <summary>
    /// The unique identifier of the employee who handled the order.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// The date and time when the order was placed.
    /// </summary>
    public DateTime OrderDate { get; set; }

    /// <summary>
    /// The total amount for the order.
    /// </summary>
    public decimal TotalAmount { get; set; }

    public List<OrderItemResponse> OrderItems { get; set; }
}