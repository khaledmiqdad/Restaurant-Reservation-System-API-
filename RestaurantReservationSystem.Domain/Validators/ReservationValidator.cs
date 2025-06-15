using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class ReservationValidator
    {
        private readonly IReservationService _reservationService;
        public ReservationValidator(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<ReservationResponse> EnsureRestaurantExistsAsync(int reservationId)
        {
            var reservation = await _reservationService.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            return reservation;
        }
    }
}