using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class OrderItemValidator
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemValidator(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        public async Task<OrderItemResponse> EnsureOrderItemExistsAsync(int orderItemId)

        {
            var orderItem = await _orderItemService.GetByIdAsync(orderItemId);
            if (orderItem == null)
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found");

            return orderItem;
        }
    }
}