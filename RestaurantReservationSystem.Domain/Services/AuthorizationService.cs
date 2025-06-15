using RestaurantReservationSystem.Domain.DTOs.Responses;
using RestaurantReservationSystem.Domain.Interfaces.Services;

namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Provides implementation of <see cref="IAuthorizationService"/> for user authentication
    /// and JWT token generation.
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationService"/> class.
        /// </summary>
        /// <param name="jwtTokenGenerator">An instance of <see cref="IJwtTokenGenerator"/> used to generate JWT tokens.</param>

        public AuthorizationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        /// <summary>
        /// Validates the provided username and password against hardcoded credentials.
        /// </summary>
        /// <param name="username">The username to validate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns><c>true</c> if the credentials match; otherwise, <c>false</c>.</returns>

        public bool ValidateCredentials(string username, string password)
        {
            return username == "admin" && password == "1234";
        }

        /// <inheritdoc />
        public LoginResponse Login(string username, string password)
        {
            if (ValidateCredentials(username, password))
            {
                var token = _jwtTokenGenerator.GenerateToken(username);
                return new LoginResponse { Success = true, Token = token };
            }

            return new LoginResponse { Success = false, Token = null };
        }
    }
}