using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validates the <see cref="EmployeeRequest"/> object using FluentValidation rules.
    /// </summary>
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRequestValidator"/> class
        /// and defines validation rules for employee creation or update requests.
        /// </summary>
        public EmployeeRequestValidator()
        {
            RuleFor(e => e.RestaurantId)
                .GreaterThan(0).WithMessage("Restaurant ID is required and must be greater than 0.");

            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(100);

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(100);

            RuleFor(e => e.Position)
                .MaximumLength(100).When(e => !string.IsNullOrEmpty(e.Position));
        }
    }

}