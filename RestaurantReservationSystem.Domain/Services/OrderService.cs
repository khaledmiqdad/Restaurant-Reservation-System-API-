using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;
using RestaurantReservationSystem.Domain.Validators;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic for managing orders, including CRUD operations and related data retrieval
    /// such as associated reservation, employee, and order items.
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderItemValidator _orderItemValidator;
        private readonly EmployeeValidator _employeeValidator;
        private readonly ReservationValidator _reservationValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="orderRepository">Repository for accessing order data.</param>
        /// <param name="mapper">AutoMapper instance for mapping between entities and DTOs.</param>
        /// <param name="orderItemRepository">Repository for accessing order item data.</param>

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IOrderItemService orderItemService,
             IReservationService reservationService, OrderItemValidator orderItemValidator,
            EmployeeValidator employeeValidator, ReservationValidator reservationValidator)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderItemValidator = orderItemValidator;
            _employeeValidator = employeeValidator;
            _reservationValidator = reservationValidator;
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> GetByIdAsync(int id)
        {
            var order = await EnsureOrderExistsAsync(id);
            return order == null ? null : _mapper.Map<OrderResponse>(order);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> CreateAsync(OrderRequest request)
        {
            var order = _mapper.Map<OrderModel>(request);
            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderResponse>(order);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> UpdateAsync(int id, OrderRequest request)
        {
            var updatedOrder = await EnsureOrderExistsAsync(id);

            _mapper.Map(request, updatedOrder);
            await _orderRepository.UpdateAsync(updatedOrder);
            return _mapper.Map<OrderResponse>(updatedOrder);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureOrderExistsAsync(id);
            var existingOrder = await _orderRepository.GetOrderByIdWithOrderItemsAsync(id);
            if (existingOrder == null) return false;

            if (existingOrder.OrderItems.Any())
                throw new InvalidOperationException("Cannot delete order with existing order items.");

            await _orderRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetOrdersByEmployeeIdAsync(int employeeId)
        {
            var employee = await _employeeValidator.EnsureEmployeeExistsAsync(employeeId);

            var orders = await _orderRepository.GetOrdersByEmployeeIdAsync(employeeId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetOrdersByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationValidator.EnsureRestaurantExistsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var orders = await _orderRepository.GetOrdersByReservationIdAsync(reservationId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<OrderResponse?> GetOrderByOrderItemIdAsync(int orderItemId)
        {
            var orderItem = await _orderItemValidator.EnsureOrderItemExistsAsync(orderItemId);

            var order = await _orderRepository.GetOrderByOrderItemIdAsync(orderItemId);
            return order == null ? null : _mapper.Map<OrderResponse>(order);
        }

        /// <inheritdoc />
        public async Task<List<OrderResponse>> GetOrdersAndMenuItemsAsync(int reservationId)
        {
            var reservation = await _reservationValidator.EnsureRestaurantExistsAsync(reservationId);

            var orders = await _orderRepository.ListOrdersAndMenuItemsAsync(reservationId);
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        /// <inheritdoc />
        public async Task<decimal> GetAverageOrderAmountAsync(int employeeId)
        {
            var employee = await _employeeValidator.EnsureEmployeeExistsAsync(employeeId);

            var amounts = await _orderRepository.GetOrderAmountsByEmployeeIdAsync(employeeId);
            if (!amounts.Any())
                return 0;

            return amounts.Average();
        }

        private async Task<OrderModel> EnsureOrderExistsAsync(int orderId)

        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            return order;
        }
    }
}