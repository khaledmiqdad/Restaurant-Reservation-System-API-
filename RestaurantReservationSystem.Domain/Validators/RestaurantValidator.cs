using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;


namespace RestaurantReservationSystem.Domain.Validators
{
    public class RestaurantValidator
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantValidator(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<RestaurantResponse> EnsureRestaurantExistsAsync(int restaurantId)
        {
            var restaurant = await _restaurantService.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new NotFoundException($"Restaurant with ID {restaurantId} not found");

            return restaurant;
        }
    }

}