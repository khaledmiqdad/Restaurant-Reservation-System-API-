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
    /// Provides business logic and service methods for managing orderItem operations.
    /// </summary>
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly MenuItemValidator _menuItemValidator;
        private readonly OrderItemValidator _orderValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for orderItem data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public OrderItemService(IOrderItemRepository repository,
            OrderItemValidator orderValidator, IMapper mapper, MenuItemValidator menuItemValidator)
        {
            _orderItemRepository = repository;
            _orderValidator = orderValidator;
            _menuItemValidator = menuItemValidator;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetAllAsync()
        {
            var orderItem = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<List<OrderItemResponse>>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse?> GetByIdAsync(int id)
        {
            var orderItem = await EnsureOrderItemExistsAsync(id);
            return orderItem == null ? null : _mapper.Map<OrderItemResponse>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse> CreateAsync(OrderItemRequest request)
        {
            var orderItem = _mapper.Map<OrderItemModel>(request);
            await _orderItemRepository.AddAsync(orderItem);
            return _mapper.Map<OrderItemResponse>(orderItem);
        }

        /// <inheritdoc />
        public async Task<OrderItemResponse?> UpdateAsync(int id, OrderItemRequest request)
        {
            var updatedorderItem = await EnsureOrderItemExistsAsync(id);

            _mapper.Map(request, updatedorderItem);
            await _orderItemRepository.UpdateAsync(updatedorderItem);
            return _mapper.Map<OrderItemResponse>(updatedorderItem);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var orderItem = await EnsureOrderItemExistsAsync(id);
            await _orderItemRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByMenuItamIdAsync(int menuItemId)
        {
            var menuItem = await _menuItemValidator.EnsureMenuItemExistsAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            var orderItems = await _orderItemRepository.GetOrderItemsByMenuItemIdAsync(menuItemId);
            return _mapper.Map<List<OrderItemResponse>>(orderItems);
        }

        /// <inheritdoc />
        public async Task<List<OrderItemResponse>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var order = await _orderValidator.EnsureOrderItemExistsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            var orders = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
            return _mapper.Map<List<OrderItemResponse>>(orders);
        }

        private async Task<OrderItemModel> EnsureOrderItemExistsAsync(int orderItemId)

        {
            var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
            if (orderItem == null)
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found");

            return orderItem;
        }
    }
}