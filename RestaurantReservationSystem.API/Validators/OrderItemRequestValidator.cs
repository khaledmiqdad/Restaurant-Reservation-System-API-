using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator for <see cref="OrderItemRequest"/> to ensure that the order item data is valid before processing.
    /// </summary>
    /// <remarks>
    /// This validator checks that:
    /// <list type="bullet">
    /// <item><description><c>OrderId</c> must be greater than 0.</description></item>
    /// <item><description><c>ItemId</c> must be greater than 0.</description></item>
    /// <item><description><c>Quantity</c> must be greater than 0, with a custom message if violated.</description></item>
    /// </list>
    /// Uses FluentValidation for clean and declarative rule definition.
    /// </remarks>
    public class OrderItemRequestValidator : AbstractValidator<OrderItemRequest>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemRequestValidator"/> class
        /// and defines validation rules for orderItem creation or update requests.
        /// </summary>
        public OrderItemRequestValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0);
            RuleFor(x => x.ItemId).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0)
            .WithMessage("Quantity must be at least 1.");
        }
    }
}