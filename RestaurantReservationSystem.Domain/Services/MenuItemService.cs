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
    /// Provides business logic and service methods for managing menuItem operations.
    /// </summary>
    public class MenuItemService : IMenuItemService
    {

        private readonly IMenuItemRepository _menuItemRepository;
        private readonly OrderItemValidator _orderItemValidator;
        private readonly ReservationValidator _reservationValidator;
        private readonly RestaurantValidator _restaurantValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for menuItem data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public MenuItemService(IMenuItemRepository repository, IMapper mapper, OrderItemValidator orderItemValidator,
            ReservationValidator reservationValidator, RestaurantValidator restaurantValidator)
        {
            _menuItemRepository = repository;
            _orderItemValidator = orderItemValidator;
            _mapper = mapper;
            _reservationValidator = reservationValidator;
            _restaurantValidator = restaurantValidator;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetAllAsync()
        {
            var menuItem = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> GetByIdAsync(int id)
        {
            var menuItem = await EnsureMenuItemExistsAsync(id);
            return _mapper.Map<MenuItemResponse>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse> CreateAsync(MenuItemRequest request)
        {
            var menuItem = _mapper.Map<MenuItemModel>(request);
            await _menuItemRepository.AddAsync(menuItem);
            return _mapper.Map<MenuItemResponse>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> UpdateAsync(int id, MenuItemRequest request)
        {
            var updatedMenuItem = await EnsureMenuItemExistsAsync(id);

            _mapper.Map(request, updatedMenuItem);
            await _menuItemRepository.UpdateAsync(updatedMenuItem);
            return _mapper.Map<MenuItemResponse>(updatedMenuItem);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureMenuItemExistsAsync(id);
            var existingMenuItem = await _menuItemRepository.GetByIdWithOrderItemsAsync(id);

            if (existingMenuItem.OrderItems.Any())
                throw new InvalidOperationException("Cannot delete menu item with existing order items.");

            await _menuItemRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetOrderedMenuItemsAsync(int reservationId)
        {
            var reservation = await _reservationValidator.EnsureRestaurantExistsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var orderedMenuItems = await _menuItemRepository.ListOrderedMenuItemsAsync(reservationId);
            return _mapper.Map<List<MenuItemResponse>>(orderedMenuItems);
        }

        /// <inheritdoc />
        public async Task<List<MenuItemResponse>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantValidator.EnsureRestaurantExistsAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            var menuItem = await _menuItemRepository.GetMenuItemsByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<MenuItemResponse>>(menuItem);
        }

        /// <inheritdoc />
        public async Task<MenuItemResponse?> GetMenuItemByOrderItemIdAsync(int orderItemId)
        {
            var orderItem = await _orderItemValidator.EnsureOrderItemExistsAsync(orderItemId);
            if (orderItem == null)
                throw new NotFoundException($"OrderItem with ID {orderItemId} not found");

            var MenuItem = await _menuItemRepository.GetMenuItemByOrderItemIdAsync(orderItemId);
            return MenuItem == null ? null : _mapper.Map<MenuItemResponse>(MenuItem);
        }

        private async Task<MenuItemModel> EnsureMenuItemExistsAsync(int menuItemId)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            return menuItem;
        }
    }
}