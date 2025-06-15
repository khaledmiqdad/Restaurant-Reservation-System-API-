using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Responses;
using RestaurantReservationSystem.Domain.Services;

namespace RestaurantReservationSystem.API.Controllers
{
    //[Authorize]
    /// <summary>
    /// Controller for managing reservation-related operations.
    /// </summary>
    [Route("api/reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _ordersService;
        private readonly IRestaurantService _restaurantService;
        private readonly ITableService _tableService;
        private readonly IMenuItemService _menuItemService;

        public ReservationsController(IReservationService reservationService, ICustomerService customerService
            , IOrderService ordersService, IRestaurantService restaurantService, ITableService tableService, IMenuItemService menuItemService)
        {
            _reservationService = reservationService;
            _ordersService = ordersService;
            _customerService = customerService;
            _restaurantService = restaurantService;
            _tableService = tableService;
            _menuItemService = menuItemService;
        }

        /// <summary>
        /// Retrieves all reservation.
        /// </summary>
        /// <returns>List of all reservation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reservations = await _reservationService.GetAllAsync();
            return Ok(ApiResponse<List<ReservationResponse>>.SuccessResponse(reservations));
        }

        /// <summary>
        /// Retrieves a specific reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>The reservation details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(reservation));
        }

        /// <summary>
        /// Creates a new reservation.
        /// </summary>
        /// <param name="request">Reservation creation request payload</param>
        /// <returns>The created reservation with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ReservationRequest request)
        {
            var createdReservation = await _reservationService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdReservation.ReservationId },
               createdReservation);
            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(createdReservation));
        }

        /// <summary>
        /// Updates an existing reservation completely.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="request">Updated reservation data</param>
        /// <returns>The updated reservation details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ReservationRequest request)
        {
            var updatedReservation = await _reservationService.UpdateAsync(id, request);
            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(updatedReservation));
        }

        /// <summary>
        /// Applies partial update to an reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated reservation details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<ReservationRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingReservation = await _reservationService.GetByIdAsync(id);

            var reservationToPatch = new ReservationRequest
            {
                CustomerId = existingReservation.CustomerId,
               RestaurantId = existingReservation.RestaurantId,
               TableId = existingReservation.TableId,
               ReservationDate = existingReservation.ReservationDate,
               PartySize = existingReservation.PartySize
            };

            patchDoc.ApplyTo(reservationToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(reservationToPatch))
                return BadRequest(ModelState);

            var updatedReservation = await _reservationService.UpdateAsync(id, reservationToPatch);
            if (updatedReservation == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update employee"));

            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(updatedReservation));
        }

        /// <summary>
        /// Deletes an reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedReservation = await _reservationService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Reservation deleted successfully"));
        }

        /// <summary>
        /// Retrieves all orders associated with the specified reservation,
        /// including their corresponding menu items.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <returns>A list of orders, each containing order items and related menu item details.</returns>
        [HttpGet("{id}/orders")]
        public async Task<IActionResult> GetOrdersAndMenuItemsAsync(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var orders = await _ordersService.GetOrdersAndMenuItemsAsync(id);
            return Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(orders));
        }

        /// <summary>
        /// Retrieves the customer to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Customer details of the reservation.</returns>
        [HttpGet("{id}/customer")]
        public async Task<IActionResult> GetCustomerAsync(int id)
        {
            var employee = await _reservationService.GetByIdAsync(id);
            if (employee == null)
                return NotFound(ApiResponse<ReservationResponse>.FailResponse("Reservation not found"));

            var customer = await _customerService.GetCustomerByReservationIdAsync(id);
            if (customer == null)
                return NotFound(ApiResponse<CustomerResponse>.FailResponse("Customer not found"));

            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(customer));
        }

        /// <summary>
        /// Retrieves the restaurant to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Restaurant details of the reservation.</returns>
        [HttpGet("{id}/restaurant")]
        public async Task<IActionResult> GetRestaurantAsync(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByReservationIdAsync(id);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        /// <summary>
        /// Retrieves the table to which the reservation belongs.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>Table details of the reservation.</returns>
        [HttpGet("{id}/table")]
        public async Task<IActionResult> GetTableAsync(int id)
        {
            var table = await _tableService.GetTableByReservationIdAsync(id);
            return Ok(ApiResponse<TableResponse>.SuccessResponse(table));
        }

        /// <summary>
        /// Retrieves the reservations handled by a specific customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>reservations handled by a specific customer.</returns>
        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetReservationsByCustomerAsync(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound(ApiResponse<CustomerResponse>.FailResponse("Customer not found"));

            var reservationsByCustomer = await _reservationService.GetReservationsByCustomerIdAsync(id);
            if (reservationsByCustomer == null)
                return NotFound(ApiResponse<TableResponse>.FailResponse("Theres no reservations for this Customer"));

            return Ok(ApiResponse<List<ReservationResponse>>.SuccessResponse(reservationsByCustomer));
        }

        /// <summary>
        /// Retrieves all Ordered Menu Items handled by the specified reservation.
        /// </summary>
        /// <param name="id">Reservation ID</param>
        /// <returns>List of Ordered Menu Items assigned to the reservation.</returns>
        [HttpGet("{id}/menu-items")]
        public async Task<IActionResult> GetOrderedMenuItemsAsync(int id)
        {
            var orderedMenuItems = await _menuItemService.GetOrderedMenuItemsAsync(id);
            return Ok(ApiResponse<List<MenuItemResponse>>.SuccessResponse(orderedMenuItems));
        }
    }
}