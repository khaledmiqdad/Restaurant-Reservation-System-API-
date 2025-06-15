using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator for <see cref="TableRequest"/> using FluentValidation.
    /// Ensures that the restaurant ID and table capacity are valid.
    /// </summary>
    public class TableRequestValidator : AbstractValidator<TableRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableRequestValidator"/> class.
        /// Defines validation rules for creating or updating a table.
        /// </summary>
        public TableRequestValidator()
        {
            RuleFor(t => t.RestaurantId)
                .GreaterThan(0)
                .WithMessage("Restaurant ID must be greater than zero.");

            RuleFor(t => t.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.");
        }
    }
}