using AutoMapper;
using RestaurantReservationSystem.Domain.DTOs.Requests;
using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Models;
using RestaurantReservationSystem.Domain.Validators;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides business logic and data manipulation for table-related operations.
    /// </summary>
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly ReservationValidator _reservationValidator;
        private readonly RestaurantValidator _restaurantValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableService"/> class.
        /// </summary>
        /// <param name="tableRepository">The repository for table data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        /// <param name="reservationService">The repository for reservation data access.</param>

        public TableService(ITableRepository tableRepository, IMapper mapper, ReservationValidator reservationValidator,
            RestaurantValidator restaurantValidator)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
            _reservationValidator = reservationValidator;
            _restaurantValidator = restaurantValidator;
        }

        /// <inheritdoc />
        public async Task<List<TableResponse>> GetAllAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return _mapper.Map<List<TableResponse>>(tables);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> GetByIdAsync(int id)
        {
            var table = await EnsureTableExistsAsync(id);
            return table == null ? null : _mapper.Map<TableResponse>(table);
        }

        /// <inheritdoc />
        public async Task<TableResponse> CreateAsync(TableRequest request)
        {
            var table = _mapper.Map<TableModel>(request);
            await _tableRepository.AddAsync(table);
            return _mapper.Map<TableResponse>(table);
        }

        /// <inheritdoc />
        public async Task<TableResponse?> UpdateAsync(int id, TableRequest request)
        {
            var updatedTable = await EnsureTableExistsAsync(id);

            _mapper.Map(request, updatedTable);
            await _tableRepository.UpdateAsync(updatedTable);
            return _mapper.Map<TableResponse>(updatedTable);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existingTable = await EnsureTableExistsAsync(id);
            await _tableRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<List<TableResponse>> GetTablesByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantValidator.EnsureRestaurantExistsAsync(restaurantId);

            var tables = await _tableRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<TableResponse>>(tables);
        }

        public async Task<TableResponse?> GetTableByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationValidator.EnsureRestaurantExistsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var table = await _tableRepository.GetTableByReservationIdAsync(reservationId);
            return table == null ? null : _mapper.Map<TableResponse>(table);
        }

        private async Task<TableModel> EnsureTableExistsAsync(int tableId)

        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            return table;
        }
    }
}