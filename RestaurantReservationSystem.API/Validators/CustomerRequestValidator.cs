using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator class for <see cref="CustomerRequest"/> DTO.
    /// Applies validation rules to ensure the customer's input data meets required constraints.
    /// </summary>
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRequestValidator"/> class.
        /// Sets validation rules for first name, last name, email, and phone number.
        /// </summary>
        public CustomerRequestValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(c => c.Email)
                .EmailAddress().When(c => !string.IsNullOrEmpty(c.Email))
                .WithMessage("Invalid email format.");

            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\d{10,15}$").When(c => !string.IsNullOrEmpty(c.PhoneNumber))
                .WithMessage("Phone number must contain only digits and be between 10 and 15 characters.");
        }
    }
}