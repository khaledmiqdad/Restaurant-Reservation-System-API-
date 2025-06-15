using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Db.Constants;
using RestaurantReservation.Db.Entities;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Models;

namespace RestaurantReservation.Db.Repositories
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly RestaurantReservationDbContext _context;
        private readonly IMapper _mapper;

        public EmployeeRepository(RestaurantReservationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeModel>> ListManagersAsync()
        {
            var employees = await _context.Employees
                                              .Where(e => e.Position == EmployeePositions.Manager)
                                              .ToListAsync();

            return _mapper.Map<List<EmployeeModel>>(employees);
        }

        public async Task<List<EmployeeModel>> GetAllAsync()
        {
            return await _context.Employees
           .ProjectTo<EmployeeModel>(_mapper.ConfigurationProvider)
           .ToListAsync();
        }

        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return _mapper.Map<EmployeeModel>(employee);
        }

        public async Task AddAsync(EmployeeModel model)
        {
            var employee = _mapper.Map<Employee>(model);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            model.EmployeeId = employee.EmployeeId;
        }

        public async Task UpdateAsync(EmployeeModel model)
        {
            var existingEmployee = await _context.Employees.FindAsync(model.EmployeeId);
            if (existingEmployee == null)
                return;

            _mapper.Map(model, existingEmployee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee is null) return;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeModel?> GetByIdWithOrdersAsync(int id)
        {
            var items = await _context.Employees
                .Include(e => e.Orders)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            return _mapper.Map<EmployeeModel>(items);
        }

        public async Task<List<EmployeeModel>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Employees
            .Where(e => e.RestaurantId == restaurantId)
            .ProjectTo<EmployeeModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<EmployeeModel?> GetEmployeeByOrderIdAsync(int orderId)
        {
            var order = await _context.Orders
                 .Include(o => o.Employee)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return _mapper.Map<EmployeeModel>(order?.Employee);
        }
    }
}