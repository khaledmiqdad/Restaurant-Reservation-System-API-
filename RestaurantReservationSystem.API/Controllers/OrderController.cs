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
    /// Controller responsible for handling HTTP requests related to Order operations.
    /// Provides endpoints for CRUD operations, partial updates, and retrieving related entities
    /// like Employee, Reservation, and Order Items.
    /// </summary>
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEmployeeService _employeeService;
        private readonly IReservationService _reservationService;
        private readonly IOrderItemService _orderItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="orderService">Service used to manage orders.</param>
        public OrdersController(IOrderService orderService, IEmployeeService employeeService, IReservationService reservationService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _employeeService = employeeService;
            _reservationService = reservationService;
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Retrieves a list of all orders.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetAllAsync()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(ApiResponse<List<OrderResponse>>.SuccessResponse(orders));
        }

        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetByIdAsync(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(ApiResponse<OrderResponse>.SuccessResponse(order));
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="request">The order data to create.</param>
        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateAsync(OrderRequest request)
        {
            var createdOrder = await _orderService.CreateAsync(request);
            CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = createdOrder.OrderId },
               createdOrder);
            return Ok(ApiResponse<OrderResponse>.SuccessResponse(createdOrder));
        }

        /// <summary>
        /// Partially updates an order using a JSON Patch document.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="patchDoc">The patch document with updates.</param>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, [FromBody] JsonPatchDocument<OrderRequest> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest(ApiResponse<string>.FailResponse("Patch document cannot be null"));

            var existingOrder = await _orderService.GetByIdAsync(id);

            var orderToPatch = new OrderRequest
            {
                ReservationId = existingOrder.ReservationId,
                EmployeeId = existingOrder.EmployeeId,
                OrderDate = existingOrder.OrderDate,
                TotalAmount = existingOrder.TotalAmount,
            };

            patchDoc.ApplyTo(orderToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.FailResponse("Invalid patch document"));

            if (!TryValidateModel(orderToPatch))
                return BadRequest(ModelState);

            var updatedOrder = await _orderService.UpdateAsync(id, orderToPatch);
            if (updatedOrder == null)
                return NotFound(ApiResponse<string>.FailResponse("Failed to update order"));

            return Ok(ApiResponse<OrderResponse>.SuccessResponse(updatedOrder));
        }

        /// <summary>
        /// Fully updates an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="request">The updated order data.</param>
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> Update(int id, OrderRequest request)
        {
            var updatedOrder = await _orderService.UpdateAsync(id, request);
            return Ok(ApiResponse<OrderResponse>.SuccessResponse(updatedOrder));
        }

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedOrder = await _orderService.DeleteAsync(id);
            return Ok(ApiResponse<string>.SuccessResponse("Order deleted successfully"));
        }

        /// <summary>
        /// Retrieves the employee responsible for a specific order.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        [HttpGet("{id}/employee")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByOrderIdAsync(id);
            return Ok(ApiResponse<EmployeeResponse>.SuccessResponse(employee));
        }

        /// <summary>
        /// Retrieves the reservation related to a specific order.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        [HttpGet("{id}/reservation")]
        public async Task<ActionResult<ReservationResponse>> GetReservation(int id)
        {
            var reservation = await _reservationService.GetReservationByOrderIdAsync(id);
            return Ok(ApiResponse<ReservationResponse>.SuccessResponse(reservation));
        }

        /// <summary>
        /// Retrieves the items associated with a specific order.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        [HttpGet("{id}/items")]
        public async Task<ActionResult<List<OrderItemResponse>>> GetOrderItems(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound(ApiResponse<OrderResponse>.FailResponse("Order not found"));

            var items = await _orderItemService.GetOrderItemsByOrderIdAsync(id);
            return Ok(ApiResponse<List<OrderItemResponse>>.SuccessResponse(items));
        }
    }
}