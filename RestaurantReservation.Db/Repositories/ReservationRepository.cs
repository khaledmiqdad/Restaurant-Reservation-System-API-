using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public ReservationRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReservationModel>> GetReservationsByCustomerIdAsync(int customerId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.CustomerId == customerId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<List<ReservationModel>> GetAllAsync()
        {
            var reservations = await _context.Reservations.ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<ReservationModel> GetByIdAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            return _mapper.Map<ReservationModel>(reservation);
        }

        public async Task AddAsync(ReservationModel reservationModel)
        {
            var reservation = _mapper.Map<Reservation>(reservationModel);
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            reservationModel.ReservationId = reservation.ReservationId;
        }

        public async Task UpdateAsync(ReservationModel reservationModel)
        {
            var existing = await _context.Reservations.FindAsync(reservationModel.ReservationId);
            if (existing is null) return;

            _context.Entry(existing).CurrentValues.SetValues(_mapper.Map<Reservation>(reservationModel));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _context.Reservations
                 .Include(r => r.Orders)
                 .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReservationModel?> GetReservationByIdWithOrdersAsync(int id)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Orders)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            return _mapper.Map<ReservationModel>(reservations);
        }

        public async Task<List<ReservationModel>> GetReservationsByRestaurantIdAsync(int restaurantId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.RestaurantId == restaurantId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }


        public async Task<List<ReservationModel>> GetReservationsByTableIdAsync(int tableId)
        {
            var reservations = await _context.Reservations
                                             .Where(r => r.TableId == tableId)
                                             .ToListAsync();
            return _mapper.Map<List<ReservationModel>>(reservations);
        }

        public async Task<ReservationModel?> GetReservationByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders
                 .Include(o => o.Reservation)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return _mapper.Map<ReservationModel>(order?.Reservation);
        }
    }
}