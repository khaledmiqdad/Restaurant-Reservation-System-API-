using RestaurantReservation.Db;
using RestaurantReservation.Db.Entities;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RestaurantReservationSystem.Domain.Models;

internal class TableRepository : ITableRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly IMapper _mapper;

    public TableRepository(RestaurantReservationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TableModel>> GetAllAsync()
    {
        return await _context.Tables
            .ProjectTo<TableModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TableModel> GetByIdAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);
        return _mapper.Map<TableModel>(table);
    }

    public async Task AddAsync(TableModel model)
    {
        var table = _mapper.Map<Table>(model);
        await _context.Tables.AddAsync(table);
        await _context.SaveChangesAsync();

        model.TableId = table.TableId;
    }

    public async Task UpdateAsync(TableModel model)
    {
        var table = await _context.Tables.FindAsync(model.TableId);
        if (table == null) return;

        _mapper.Map(model, table);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var table = await _context.Tables.FindAsync(id);
        if (table != null)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TableModel>> GetByRestaurantIdAsync(int restaurantId)
    {
        return await _context.Tables
            .Where(e => e.RestaurantId == restaurantId)
            .ProjectTo<TableModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TableModel?> GetTableByReservationIdAsync(int reservationId)
    {
        var reservations = await _context.Reservations
            .Include(r => r.Table)
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

        return _mapper.Map<TableModel>(reservations?.Table);
    }
}