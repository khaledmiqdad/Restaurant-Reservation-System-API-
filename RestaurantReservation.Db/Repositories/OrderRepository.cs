using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories
{
    internal class OrderRepository : IOrderRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderModel>> ListOrdersAndMenuItemsAsync(int reservationId)
        {
            var orders = await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<List<decimal>> GetOrderAmountsByEmployeeIdAsync(int employeeId)
        {
            return await _context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .Select(o => o.TotalAmount)
                .ToListAsync();
        }

        public async Task<List<OrderModel>> GetAllAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return _mapper.Map<List<OrderModel>>(orders);
        }


        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            return _mapper.Map<OrderModel>(order);
        }

        public async Task AddAsync(OrderModel orderModel)
        {
            var order = _mapper.Map<Order>(orderModel);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderModel.OrderId = order.OrderId;
        }


        public async Task UpdateAsync(OrderModel orderModel)
        {
            var existingOrder = await _context.Orders.FindAsync(orderModel.OrderId);
            if (existingOrder == null)
                return;

            _context.Entry(existingOrder).CurrentValues.SetValues(_mapper.Map<Order>(orderModel));
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order is null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<OrderModel?> GetOrderByIdWithOrderItemsAsync(int id)
        {
            var items = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            return _mapper.Map<OrderModel>(items);
        }

        public async Task<List<OrderModel>> GetOrdersByEmployeeIdAsync(int employeeId)
        {
            var orders = await _context.Orders
                .Where(o => o.EmployeeId == employeeId)
                .ToListAsync();

            return _mapper.Map<List<OrderModel>>(orders);
        }

        public async Task<List<OrderModel>> GetOrdersByReservationIdAsync(int reservationId)
        {
            return await _context.Orders
                .Where(o => o.ReservationId == reservationId)
                .ProjectTo<OrderModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<OrderModel?> GetOrderByOrderItemIdAsync(int orderItemId)
        {
            var menuItem = await _context.OrderItems
                .Include(o => o.Order)
                .FirstOrDefaultAsync(e => e.OrderItemId == orderItemId);

            return _mapper.Map<OrderModel>(menuItem?.Order);
        }
    }
}