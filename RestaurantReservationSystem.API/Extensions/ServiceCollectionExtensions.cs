using RestaurantReservationSystem.Domain.Configurations;

namespace RestaurantReservationSystem.API.Extensions
{
    /// <summary>
    /// Provides extension methods for registering application services and configurations.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers application-specific services, AutoMapper, JWT configuration, and repositories.
        /// </summary>
        /// <param name="services">The service collection to which services will be added.</param>
        /// <param name="configuration">The application configuration containing necessary settings.</param>
        /// <returns>The updated service collection with registered services.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            services.AddRepositories(configuration.GetConnectionString("DefaultConnection"));
            services.AddDomainServices(jwtSettings);

            return services;
        }
    }
}