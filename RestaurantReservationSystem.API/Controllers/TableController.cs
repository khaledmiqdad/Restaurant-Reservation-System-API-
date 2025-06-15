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
    /// Controller for managing restaurant tables.
    /// </summary>
    [ApiController]
    [Route("api/tables")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IRestaurantService _restaurantService;
        private readonly IReservationService _reservationService;

        public TableController(ITableService tableService, IRestaurantService restaurantService, IReservationService reservationService)
        {
            _tableService = tableService;
            _restaurantService = restaurantService;
            _reservationService = reservationService;
        }

        /// <summary>
        /// Get a list of all tables.
        /// </summary>
        /// <returns>List of tables.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tables = await _tableService.GetAllAsync();
            return Ok(ApiResponse<List<TableResponse>>.SuccessResponse(tables));
        }

        /// <summary>
        /// Get a table by its ID.
        /// </summary>
        /// <param name="id">The ID of the table.</param>
        /// <returns>The table data if found; otherwise, 404.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var table = await _tableService.GetByIdAsync(id);

            return Ok(ApiResponse<TableResponse>.SuccessResponse(table));
        }

        /// <summary>
        /// Create a new table.
        /// </summary>
        /// <param name="request">Table creation request payload.</param>
        /// <returns>The created table data with 201 status.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TableRequest request)
        {
            var createdTable = await _tableService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdTable.TableId },
                createdTable);

            return Ok(ApiResponse<TableResponse>.SuccessResponse(createdTable));
        }

        /// <summary>
        /// Update a table completely.
        /// </summary>
        /// <param name="id">The ID of the table to update.</param>
        /// <param name="request">Updated table data.</param>
        /// <returns>The updated table or 404 if not found.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] TableRequest request)
        {
            var updatedTable = await _tableService.UpdateAsync(id, request);

            return Ok(ApiResponse<TableResponse>.SuccessResponse(updatedTable));
        }

        /// <summary>
        /// Partially update a table using JSON Patch.
        /// </summary>
        /// <param name="id">ID of the table to patch.</param>
        /// <param name="patchDoc">JSON Patch document.</param>
        /// <returns>The updated table if successful, or appropriate error response.</returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<TableRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingTable = await _tableService.GetByIdAsync(id);

            var tableToPatch = new TableRequest
            {
                RestaurantId = existingTable.RestaurantId,
                Capacity = existingTable.Capacity
            };

            patchDoc.ApplyTo(tableToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(tableToPatch))
                return BadRequest(ModelState);

            var updatedTable = await _tableService.UpdateAsync(id, tableToPatch);
            if (updatedTable == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update table"));

            return Ok(ApiResponse<TableResponse>.SuccessResponse(updatedTable));
        }

        /// <summary>
        /// Delete a table by its ID.
        /// </summary>
        /// <param name="id">The ID of the table to delete.</param>
        /// <returns>Success message or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedTable = await _tableService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Table deleted successfully"));
        }

        /// <summary>
        /// Get the restaurant to which a table belongs.
        /// </summary>
        /// <param name="id">The ID of the table.</param>
        /// <returns>Restaurant details or 404 if not found.</returns>
        [HttpGet("{id}/restaurant")]
        public async Task<IActionResult> GetRestaurantAsync(int id)
        {
            var restaurant = await _restaurantService.GetRestaurantByTableIdAsync(id);
            return Ok(ApiResponse<RestaurantResponse>.SuccessResponse(restaurant));
        }

        /// <summary>
        /// Get all reservations for a specific table.
        /// </summary>
        /// <param name="id">The ID of the table.</param>
        /// <returns>List of reservations for the table.</returns>
        [HttpGet("{id}/reservations")]
        public async Task<IActionResult> GetReservationsAsync(int id)
        {
            var reservations = await _reservationService.GetReservationsByTableIdAsync(id);
            return Ok(ApiResponse<List<ReservationResponse>>.SuccessResponse(reservations));
        }
    }
}