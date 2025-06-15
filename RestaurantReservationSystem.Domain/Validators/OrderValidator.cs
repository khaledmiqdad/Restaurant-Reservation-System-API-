using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class OrderValidator
    {
        private readonly IOrderService _orderService;
        public OrderValidator(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<OrderResponse> EnsureOrderExistsAsync(int orderId)

        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            return order;
        }
    }
}