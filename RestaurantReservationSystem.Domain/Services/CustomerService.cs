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
    /// Provides business logic and service methods for managing customer operations.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ReservationValidator _reservationValidator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="repository">The repository responsible for customer data access.</param>
        /// <param name="mapper">The mapper used to convert between entities and DTOs.</param>
        public CustomerService(ICustomerRepository repository, IMapper mapper, ReservationValidator reservationValidator)
        {
            _customerRepository = repository;
            _mapper = mapper;
            _reservationValidator = reservationValidator;
        }

        /// <inheritdoc />
        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customer = await _customerRepository.GetAllAsync();
            return _mapper.Map<List<CustomerResponse>>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> GetByIdAsync(int id)
        {
            var customer = await EnsureCustomerExistsAsync(id);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse> CreateAsync(CustomerRequest request)
        {
            var customer = _mapper.Map<CustomerModel>(request);
            await _customerRepository.AddAsync(customer);
            return _mapper.Map<CustomerResponse>(customer);
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> UpdateAsync(int id, CustomerRequest request)
        {
            var updatedCustomer = await EnsureCustomerExistsAsync(id);

            _mapper.Map(request, updatedCustomer);
            await _customerRepository.UpdateAsync(updatedCustomer);
            return _mapper.Map<CustomerResponse>(updatedCustomer);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await EnsureCustomerExistsAsync(id);
            await _customerRepository.DeleteAsync(id);
            return true;
        }

        /// <inheritdoc />
        public async Task<CustomerResponse?> GetCustomerByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationValidator.EnsureRestaurantExistsAsync(reservationId);
            if (reservation == null)
                throw new NotFoundException($"Reservation with ID {reservationId} not found");

            var customer = await _customerRepository.GetCustomerByReservationIdAsync(reservationId);
            return customer == null ? null : _mapper.Map<CustomerResponse>(customer);
        }

        private async Task<CustomerModel> EnsureCustomerExistsAsync(int customerId)

        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {customerId} not found");

            return customer;
        }
    }
}