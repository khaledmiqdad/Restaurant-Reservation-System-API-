using Microsoft.AspNetCore.Mvc;
using RestaurantReservationSystem.Domain.Exceptions;
namespace RestaurantReservationSystem.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            var (statusCode, title, detail) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource not found", exception.Message),
                UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized", "Access is denied"),
                InvalidOperationException => (StatusCodes.Status400BadRequest, "Invalid operation", exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred", "Internal server error")
            };

            logger.LogError(exception, title);

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Status = statusCode,
                Detail = detail,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }
    }
}