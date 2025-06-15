using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator for the <see cref="OrderRequest"/> DTO.
    /// Ensures that the request contains valid and consistent data
    /// before processing an order.
    /// </summary>
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRequestValidator"/> class
        /// and defines validation rules for the order request properties.
        /// </summary>
        public OrderRequestValidator()
        {
            RuleFor(x => x.ReservationId)
                .GreaterThan(0)
                .WithMessage("Reservation ID must be greater than 0.");

            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("Employee ID must be greater than 0.");

            RuleFor(x => x.OrderDate)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Order date must be now or earlier.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total amount must be 0 or more.");
        }
    }
}