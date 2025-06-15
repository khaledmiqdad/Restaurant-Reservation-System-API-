using Microsoft.Extensions.DependencyInjection;
using RestaurantReservationSystem.Domain.Configurations;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using RestaurantReservationSystem.Domain.Services;
using RestaurantReservationSystem.Domain.Validators;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, JwtSettings jwtSettings)
    {
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderItemService, OrderItemService>();

        services.AddScoped<TableValidator>();
        services.AddScoped<RestaurantValidator>();
        services.AddScoped<ReservationValidator>();
        services.AddScoped<OrderItemValidator>();
        services.AddScoped<OrderValidator>();
        services.AddScoped<EmployeeValidator>();
        services.AddScoped<MenuItemValidator>();

        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(jwtSettings));


        return services;
    }
}