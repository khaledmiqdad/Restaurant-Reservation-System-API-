using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Responses;

namespace RestaurantReservationSystem.API.Controllers
{
    //[Authorize]
    /// <summary>
    /// API controller for managing restaurants.
    /// Provides endpoints to create, retrieve, update, and delete restaurant data.
    /// </summary>
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITableService _tableService;
        private readonly IEmployeeService _employeeService;
        private readonly IMenuItemService _menuItemService;
        private readonly IReservationService _reservationService;

        public RestaurantsController(IRestaurantService restaurantService, ITableService tableService, 
            IEmployeeService employeeService, IMenuItemService menuItemService, IReservationService reservationService)
        {                                                                                                                                
            _restaurantService = restaurantService;
            _tableService = tableService;
            _employeeService = employeeService;
            _menuItemService = menuItemService;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Retrieves a list of all restaurants.
        /// </summary>
        /// <returns>A list of restaurants wrapped in an API response.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var restaurants = await _restaurantService.GetAllAsync();
            return Ok(ApiResponse<List<RestaurantResponse>>.SuccessResponse(restaurants));
        }

        /// <summary>
        /// Retrieves a restaurant by its unique identifier.
        /// </summary>
        /// <param name="id">The ID of the restaurant.</param>
        /// <returns>The restaurant details or a 404 error if not found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var restaurant = await _restaurantService.GetByIdAsync(id);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="request">The restaurant details to create.</param>
        /// <returns>The created restaurant with its assigned ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RestaurantRequest request)
        {
            var createdRestaurant = await _restaurantService.CreateAsync(request);

            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdRestaurant.RestaurantId },
                createdRestaurant);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(createdRestaurant));
        }

        /// <summary>
        /// Updates an existing restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to update.</param>
        /// <param name="request">The updated restaurant details.</param>
        /// <returns>The updated restaurant or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RestaurantRequest request)
        {
            var updatedRestaurant = await _restaurantService.UpdateAsync(id, request);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(updatedRestaurant));
        }

        /// <summary>
        /// Applies a JSON Patch to a restaurant.
        /// </summary>
        /// <param name="id">The ID of the restaurant to patch.</param>
        /// <param name="patchDoc">The JSON Patch document containing the operations.</param>
        /// <returns>The updated restaurant or appropriate error messages.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<RestaurantRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingRestaurant = await _restaurantService.GetByIdAsync(id);

            var restaurantToPatch = new RestaurantRequest
            {
                Name = existingRestaurant.Name,
                Address = existingRestaurant.Address,
                PhoneNumber = existingRestaurant.PhoneNumber,
                OpeningHours = existingRestaurant.OpeningHours
            };

            patchDoc.ApplyTo(restaurantToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(restaurantToPatch))
                return BadRequest(ModelState);

            var updatedRestaurant = await _restaurantService.UpdateAsync(id, restaurantToPatch);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(updatedRestaurant));
        }

        /// <summary>
        /// Deletes a restaurant by ID.
        /// </summary>
        /// <param name="id">The ID of the restaurant to delete.</param>
        /// <returns>Success message or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _restaurantService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Restaurant deleted successfully"));
        }

        /// <summary>
        /// Retrieves all employees working at the specified restaurant.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a successful <see cref="ApiResponse{T}"/> with a list
        /// of <see cref="EmployeeResponse"/> if the restaurant exists; otherwise, a not found response.
        /// </returns>
        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetEmployeesAsync(int id)
        {
            var employees = await _employeeService.GetEmployeesByRestaurantIdAsync(id);
            return Ok(ApiResponse<List<EmployeeResponse>>.SuccessResponse(employees));
        }


        /// <summary>
        /// Retrieves all tables associated with the specified restaurant.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a successful <see cref="ApiResponse{T}"/> 
        /// with a list of <see cref="TableResponse"/> objects representing the tables of the restaurant.
        /// </returns>
        [HttpGet("{id}/tables")]
        public async Task<IActionResult> GetTablesAsync(int id)
        {
            var tables = await _tableService.GetTablesByRestaurantIdAsync(id);
            return Ok(ApiResponse<List<TableResponse>>.SuccessResponse(tables));
        }

        /// <summary>
        /// Retrieves all menu items offered by a specific restaurant.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a successful <see cref="ApiResponse{T}"/> 
        /// with a list of <see cref="MenuItemResponse"/> if the restaurant exists; otherwise, a not found response.
        /// </returns>
        [HttpGet("{id}/menu-items")]
        public async Task<IActionResult> GetMenuItemsAsync(int id)
        {
            var menuItems = await _menuItemService.GetMenuItemsByRestaurantIdAsync(id);
            return Ok(ApiResponse<List<MenuItemResponse>>.SuccessResponse(menuItems));
        }

        /// <summary>
        /// Retrieves all reservations made for the specified restaurant.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <returns>
        /// An <see cref="IActionResult"/> containing a successful <see cref="ApiResponse{T}"/> with a list
        /// of <see cref="ReservationResponse"/> objects representing the reservations for the restaurant.
        /// </returns>
        [HttpGet("{id}/reservations")]
        public async Task<IActionResult> GetReservationsAsync(int id)
        {
            var reservations = await _reservationService.GetReservationsByRestaurantIdAsync(id);
            return Ok(ApiResponse<List<ReservationResponse>>.SuccessResponse(reservations));
        }
    }
}