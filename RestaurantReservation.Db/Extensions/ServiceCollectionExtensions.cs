using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantReservation.Db;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Db.Repositories.ReportRepositories;
using RestaurantReservation.Db.Seeders;
using RestaurantReservationSystem.Db.Mapping;
using RestaurantReservationSystem.Domain.Interfaces.Repositories;
using RestaurantReservationSystem.Domain.Interfaces.Repositories.Reports;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {

        services.AddDbContext<RestaurantReservationDbContext>(options =>
                options.UseSqlServer(connectionString));

        services.AddScoped<IRestaurantReservationSeeder, RestaurantReservationSeeder>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ITableRepository, TableRepository>();

        services.AddScoped<IReservationReportRepository, ReservationReportRepository>();
        services.AddScoped<IEmployeeReportRepository, EmployeeReportRepository>();
        services.AddScoped<IRevenueReportRepository, RevenueReportRepository>();
        services.AddScoped<ICustomerReportRepository, CustomerReportRepository>();

        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        return services;
    }
}
