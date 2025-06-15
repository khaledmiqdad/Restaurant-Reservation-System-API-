using RestaurantReservationSystem.Domain.DTOs.Responses;

namespace RestaurantReservationSystem.Domain.Interfaces.Services
{
    /// <summary>
    /// Defines methods for user authorization and token generation.
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Attempts to authenticate a user with the given username and password.
        /// </summary>
        /// <param name="username">The username of the user attempting to log in.</param>
        /// <param name="password">The password associated with the username.</param>
        /// <returns>
        /// A tuple containing a success flag and a JWT token string if authentication succeeds;
        /// otherwise, the token will be null.
        /// </returns>
        LoginResponse Login(string username, string password);
    }
}