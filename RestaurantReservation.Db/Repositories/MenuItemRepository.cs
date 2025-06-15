using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;
using System.Collections.Generic;

namespace RestaurantReservation.Db.Repositories
{
    internal class MenuItemRepository : IMenuItemRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public MenuItemRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MenuItemModel>> ListOrderedMenuItemsAsync(int reservationId)
        {
            var orderedItems = await _context.OrderItems
                .Where(oi => oi.Order.ReservationId == reservationId)
                .Include(oi => oi.MenuItem)
                .Select(oi => oi.MenuItem)
                .Distinct()
                .ToListAsync();

            return _mapper.Map<List<MenuItemModel>>(orderedItems);
        }

        public async Task<List<MenuItemModel>> GetAllAsync()
        {
            var items = await _context.MenuItems.ToListAsync();
            return _mapper.Map<List<MenuItemModel>>(items);
        }

        public async Task<MenuItemModel> GetByIdAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            return _mapper.Map<MenuItemModel>(item);
        }

        public async Task AddAsync(MenuItemModel model)
        {
            var menuItem = _mapper.Map<MenuItem>(model);
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            model.ItemId = menuItem.ItemId;
        }

        public async Task UpdateAsync(MenuItemModel menuItem)
        {
            var existingItem = await _context.MenuItems.FindAsync(menuItem.ItemId);
            if (existingItem == null)
                return;

            _mapper.Map(menuItem, existingItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.MenuItems
                .Include(m => m.OrderItems)
                .FirstOrDefaultAsync(m => m.ItemId == id);

            if (menuItem is null) return;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<MenuItemModel>> GetMenuItemsByRestaurantIdAsync(int restaurantId)
        {
            var items = await _context.MenuItems
                .Where(e => e.RestaurantId == restaurantId)
                .ToListAsync();

            return _mapper.Map<List<MenuItemModel>>(items);
        }


        public async Task<MenuItemModel?> GetMenuItemByOrderItemIdAsync(int orderItemId)
        {
            var orderItems = await _context.OrderItems
                .Include(o => o.MenuItem)
                .FirstOrDefaultAsync(e => e.OrderItemId == orderItemId);

            return _mapper.Map<MenuItemModel>(orderItems?.MenuItem);
        }

        public async Task<MenuItemModel?> GetByIdWithOrderItemsAsync(int id)
        {
            var items =  await _context.MenuItems
                .Include(m => m.OrderItems)
                .FirstOrDefaultAsync(m => m.ItemId == id);

            return _mapper.Map<MenuItemModel>(items);
        }

    }
}