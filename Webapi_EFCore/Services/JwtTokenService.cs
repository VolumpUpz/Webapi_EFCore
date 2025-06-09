using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly Dictionary<string, string> _refreshTokens = new();

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var claims = new[] {
            //new Claim(ClaimTypes.Name, user.Username),
            //new Claim(ClaimTypes.Role, user.Role)
            var claims = new[] {
                new Claim("Name", user.Username),
                new Claim("Test", "test"),
                new Claim("Role", user.Role.ToUpperInvariant())
             };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:AccessTokenExpirationMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken(string username)


        {
            var token = Guid.NewGuid().ToString();
            _refreshTokens[username] = token;
            return token;
        }

        public bool ValidateRefreshToken(string username, string refreshToken)
        {
            return _refreshTokens.TryGetValue(username, out var validToken) && validToken == refreshToken;
        }
    }
}
