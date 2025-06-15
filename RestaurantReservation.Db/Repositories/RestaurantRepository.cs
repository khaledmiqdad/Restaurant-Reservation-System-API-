using RestaurantReservation.Db.Entities;
using RestaurantReservation.Db;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RestaurantReservationSystem.Domain.Models;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;

internal class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly IMapper _mapper;

    public RestaurantRepository(RestaurantReservationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RestaurantModel>> GetAllAsync()
    {
        return await _context.Restaurants
            .ProjectTo<RestaurantModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<RestaurantModel> GetByIdAsync(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        return _mapper.Map<RestaurantModel>(restaurant);
    }


    public async Task AddAsync(RestaurantModel model)
    {
        var restaurant = _mapper.Map<Restaurant>(model);
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();

        model.RestaurantId = restaurant.RestaurantId;
    }

    public async Task UpdateAsync(RestaurantModel model)
    {
        var restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);
        if (restaurant == null) return;

        _mapper.Map(model, restaurant);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<RestaurantModel?> GetRestaurantByEmployeeIdAsync(int employeeId)
    {
        var employee = await _context.Employees
            .Include(e => e.Restaurant)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);

        return _mapper.Map<RestaurantModel>(employee?.Restaurant);
    }

    public async Task<RestaurantModel?> GetRestaurantByTableIdAsync(int tableId)
    {
        var table = await _context.Tables
            .Include(e => e.Restaurant)
            .FirstOrDefaultAsync(e => e.TableId == tableId);

        return _mapper.Map<RestaurantModel>(table?.Restaurant);
    }

    public async Task<RestaurantModel?> GetRestaurantByMenuItemIdAsync(int menuItemId)
    {
        var menuItem = await _context.MenuItems
            .Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.ItemId == menuItemId);

        return _mapper.Map<RestaurantModel>(menuItem?.Restaurant);
    }

    public async Task<RestaurantModel?> GetRestaurantByReservationIdAsync(int reservationId)
    {
        var reservations = await _context.Reservations
            .Include(r => r.Restaurant)
            .FirstOrDefaultAsync(r => r.ReservationId == reservationId);

        return _mapper.Map<RestaurantModel>(reservations?.Restaurant);
    }
}