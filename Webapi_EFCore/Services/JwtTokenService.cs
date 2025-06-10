using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Webapi_EFCore.Data;
using Webapi_EFCore.Models;

namespace Webapi_EFCore.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly Dictionary<string, string> _refreshTokens = new();
        private readonly ApplicationDbContext _context;

        public JwtTokenService(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
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
            return Guid.NewGuid().ToString();
        }

        public async Task<string> GenerateAndSaveRefreshTokenAsync(string username)
        {
            var token = Guid.NewGuid().ToString();
            var refreshToken = new RefreshToken
            {
                Token = token,
                Username = username,
                ExpiryDate = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:RefreshTokenExpirationMinutes"])),
                IsRevoked = false
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token, string username)
        {
            var rt = await _context.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == token && t.Username == username && !t.IsRevoked);

            return rt != null && rt.ExpiryDate > DateTime.UtcNow;
        }

        public async Task RevokeTokenAsync(string token)
        {
            var rt = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
            if (rt != null)
            {
                //update
                rt.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
