using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Exceptions;
using RestaurantReservationSystem.Domain.Interfaces.Services;



namespace RestaurantReservationSystem.Domain.Validators
{
    public class EmployeeValidator
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeValidator(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public async Task<EmployeeResponse> EnsureEmployeeExistsAsync(int employeeId)
        {
            var employee = await _employeeService.GetByIdAsync(employeeId);
            if (employee == null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            return employee;
        }
    }
}
