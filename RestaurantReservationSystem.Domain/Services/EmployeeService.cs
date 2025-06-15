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
    /// Provides business logic and service methods for managing employee operations.
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly OrderValidator _orderValidator;
        private readonly RestaurantValidator _restaurantValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for employee data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public EmployeeService(IEmployeeRepository repository, IMapper mapper, IOrderService orderService
            , IRestaurantService restaurantService, OrderValidator orderValidator, RestaurantValidator restaurantValidator)
        {
            _employeeRepository = repository;
            _restaurantValidator = restaurantValidator;
            _orderValidator = orderValidator;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> ListManagersAsync()
        {
            var managers = await _employeeRepository.ListManagersAsync();
            return _mapper.Map<List<EmployeeResponse>>(managers);
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> GetByIdAsync(int id)
        {
            var employee = await EnsureEmployeeExistsAsync(id);
            return _mapper.Map<EmployeeResponse>(employee);

        }

        /// <inheritdoc />
        public async Task<EmployeeResponse> CreateAsync(EmployeeRequest request)
        {
            var employee = _mapper.Map<EmployeeModel>(request);
            await _employeeRepository.AddAsync(employee);
            return _mapper.Map<EmployeeResponse>(employee);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> UpdateAsync(int id, EmployeeRequest request)
        {
            var updatedEmployee = await EnsureEmployeeExistsAsync(id);

            _mapper.Map(request, updatedEmployee);
            await _employeeRepository.UpdateAsync(updatedEmployee);
            return _mapper.Map<EmployeeResponse>(updatedEmployee);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var existingEemployee = await EnsureEmployeeExistsAsync(id);
            await _employeeRepository.DeleteAsync(id);
                return true;
        }

        /// <inheritdoc />
        public async Task<List<EmployeeResponse>> GetEmployeesByRestaurantIdAsync(int restaurantId)
        {
            var restaurant = await _restaurantValidator.EnsureRestaurantExistsAsync(restaurantId);

            var employees = await _employeeRepository.GetByRestaurantIdAsync(restaurantId);
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }

        /// <inheritdoc />
        public async Task<EmployeeResponse?> GetEmployeeByOrderIdAsync(int orderId)
        {
            var order = await _orderValidator.EnsureOrderExistsAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            var employee = await _employeeRepository.GetEmployeeByOrderIdAsync(orderId);
            return employee == null ? null : _mapper.Map<EmployeeResponse>(employee);
        }

        private async Task<EmployeeModel> EnsureEmployeeExistsAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            return employee;
        }
    }
}