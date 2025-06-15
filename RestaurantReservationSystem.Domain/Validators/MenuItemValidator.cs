using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class MenuItemValidator
    {
        private readonly IMenuItemService _menuItemService;
        public MenuItemValidator(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public async Task<MenuItemResponse> EnsureMenuItemExistsAsync(int menuItemId)
        {
            var menuItem = await _menuItemService.GetByIdAsync(menuItemId);
            if (menuItem == null)
                throw new NotFoundException($"MenuItem with ID {menuItemId} not found");

            return menuItem;
        }
    }
}