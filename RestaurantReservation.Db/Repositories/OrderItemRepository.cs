using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories;

internal class OrderItemRepository : IOrderItemRepository
{
    private readonly RestaurantReservationDbContext _context;
    private readonly IMapper _mapper;

    public OrderItemRepository(RestaurantReservationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderItemModel>> GetAllAsync()
    {
        var orderItems = await _context.OrderItems.ToListAsync();
        return _mapper.Map<List<OrderItemModel>>(orderItems);
    }

    public async Task<OrderItemModel> GetByIdAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        return _mapper.Map<OrderItemModel>(orderItem);
    }


    public async Task AddAsync(OrderItemModel orderItemModel)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemModel);
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        orderItemModel.OrderItemId = orderItem.OrderItemId;
    }


    public async Task UpdateAsync(OrderItemModel orderItemModel)
    {
        var existingOrderItem = await _context.OrderItems.FindAsync(orderItemModel.OrderItemId);
        if (existingOrderItem == null)
            return;

        _context.Entry(existingOrderItem).CurrentValues.SetValues(_mapper.Map<OrderItem>(orderItemModel));
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem is null) return;

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<OrderItemModel>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(o => o.OrderId == orderId)
            .ProjectTo<OrderItemModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<List<OrderItemModel>> GetOrderItemsByMenuItemIdAsync(int menuItemId)
    {
        return await _context.OrderItems
            .Where(o => o.MenuItem.ItemId == menuItemId)
            .ProjectTo<OrderItemModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}