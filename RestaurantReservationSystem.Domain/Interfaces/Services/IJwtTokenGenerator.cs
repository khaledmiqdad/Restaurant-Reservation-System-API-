
namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines methods to generate and validate JSON Web Tokens (JWT).
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates a signed JWT token for the specified username.
        /// </summary>
        /// <param name="username">The username for whom the token will be generated.</param>
        /// <returns>A signed JWT token string.</returns>
        string GenerateToken(string username);
    }
}