using Microsoft.IdentityModel.Tokens;
using RestaurantReservationSystem.Domain.Configurations;
using RestaurantReservationSystem.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace RestaurantReservationSystem.Domain.Services
{
    /// <summary>
    /// Responsible for generating and validating JWT tokens.
    /// Uses symmetric key signing with HMAC SHA256 algorithm.
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Initializes a new instance of <see cref="JwtTokenGenerator"/> using the specified JWT settings.
        /// </summary>
        /// <param name="jwtSettings">An object containing the secret key, issuer, and audience used for generating JWT tokens.</param>
        public JwtTokenGenerator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Generates a signed JWT token for the specified username.
        /// The token includes standard claims like subject and unique identifier,
        /// and expires 1 hour after creation.
        /// </summary>
        /// <param name="username">The username for whom the token is generated.</param>
        /// <returns>A signed JWT token string.</returns>
        public string GenerateToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
