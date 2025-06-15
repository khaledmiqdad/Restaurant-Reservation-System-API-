using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator for <see cref="MenuItemRequest"/> to ensure the integrity of menu item data.
    /// </summary>
    public class MenuItemRequestValidator : AbstractValidator<MenuItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemRequestValidator"/> class.
        /// Defines validation rules for creating or updating a menu item.
        /// </summary>
        public MenuItemRequestValidator()
        {
            RuleFor(x => x.RestaurantId)
                .GreaterThan(0).WithMessage("Restaurant ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => x.Description != null);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}