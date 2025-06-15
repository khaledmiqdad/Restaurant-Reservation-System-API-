using FluentValidation;
using RestaurantReservationSystem.Domain.DTOs.Requests;

namespace RestaurantReservationSystem.API.Validators
{
    /// <summary>
    /// Validator for the <see cref="ReservationRequest"/> DTO.
    /// Ensures that all reservation fields meet the required business rules.
    /// </summary>
    public class ReservationRequestValidator : AbstractValidator<ReservationRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationRequestValidator"/> class.
        /// Defines validation rules for each property in the <see cref="ReservationRequest"/>.
        /// </summary>
        public ReservationRequestValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0)
                .WithMessage("Customer ID must be greater than 0."); ;

            RuleFor(x => x.RestaurantId).GreaterThan(0)
                .WithMessage("Restaurant ID must be greater than 0."); ;

            RuleFor(x => x.TableId).GreaterThan(0)
                .WithMessage("Table ID must be greater than 0."); ;

            RuleFor(x => x.ReservationDate)
                .Must(date => date > DateTime.Now)
                .WithMessage("Reservation date must be in the future.");

            RuleFor(x => x.PartySize)
                .GreaterThan(0)
                .WithMessage("Party size must be greater than 0.");
        }
    }
}