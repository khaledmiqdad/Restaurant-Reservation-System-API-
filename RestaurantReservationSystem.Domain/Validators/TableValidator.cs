using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Validators
{
    public class TableValidator
    {
        private readonly ITableService _tableService;

        public TableValidator(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task<TableResponse> EnsureTableExistsAsync(int tableId)
        {
            var table = await _tableService.GetByIdAsync(tableId);
            if (table == null)
                throw new NotFoundException($"Table with ID {tableId} not found");

            return table;
        }
    }

}