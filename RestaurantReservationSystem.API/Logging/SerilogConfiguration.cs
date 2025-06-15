using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace RestaurantReservationSystem.API.Logging
{
    /// <summary>
    /// Provides configuration setup for Serilog logging.
    /// </summary>
    public static class SerilogConfiguration
    {
        /// <summary>
        /// Configures Serilog logger based on the provided application configuration.
        /// Sets minimum logging levels, enriches logs with context, and configures outputs to console and rolling file.
        /// </summary>
        /// <param name="configuration">The application configuration containing logging settings.</param>
        public static void ConfigureSerilog(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration) 
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();
        }
    }
}