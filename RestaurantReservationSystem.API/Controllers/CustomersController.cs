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
    /// Controller for managing customer-related operations.
    /// </summary>
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IReservationService _reservationService;
        public CustomersController(ICustomerService customerService, IReservationService reservationService)
        {
            _customerService = customerService;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>List of all customers.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var customer = await _customerService.GetAllAsync();
            return Ok(ApiResponse<List<CustomerResponse>>.SuccessResponse(customer));
        }

        /// <summary>
        /// Retrieves a specific customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>The customer details if found.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(customer));
        }

        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="request">Customer creation request payload</param>
        /// <returns>The created customer with location header.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerRequest request)
        {
            var createdCustomer = await _customerService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdCustomer.CustomerId },
               createdCustomer);
            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(createdCustomer));
        }

        /// <summary>
        /// Updates an existing customer completely.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="request">Updated customer data</param>
        /// <returns>The updated customer details.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CustomerRequest request)
        {
            var updatedEmployee = await _customerService.UpdateAsync(id, request);
            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(updatedEmployee));
        }

        /// <summary>
        /// Applies partial update to an customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <param name="patchDoc">JSON Patch document</param>
        /// <returns>The updated customer details.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<CustomerRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingCustomer = await _customerService.GetByIdAsync(id);

            var customerToPatch = new CustomerRequest
            {
                FirstName = existingCustomer.FirstName,
                LastName = existingCustomer.LastName,
                Email = existingCustomer.Email,
                PhoneNumber = existingCustomer.PhoneNumber
            };

            patchDoc.ApplyTo(customerToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(customerToPatch))
                return BadRequest(ModelState);

            var updatedCustomer = await _customerService.UpdateAsync(id, customerToPatch);
            if (updatedCustomer == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update customer"));

            return Ok(ApiResponse<CustomerResponse>.SuccessResponse(updatedCustomer));
        }

        /// <summary>
        /// Deletes an customer by ID.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Success message if deleted.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedCustomer = await _customerService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Customer deleted successfully"));
        }

        /// <summary>
        /// Retrieves all reservation handled by the specified customer.
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>List of orders assigned to the customer.</returns>
        [HttpGet("{id}/reservations")]
        public async Task<IActionResult> GetReservationsAsync(int id)
        {
            var reservations = await _reservationService.GetReservationsByCustomerIdAsync(id);
            return Ok(ApiResponse<List<ReservationResponse>>.SuccessResponse(reservations));
        }
    }
}