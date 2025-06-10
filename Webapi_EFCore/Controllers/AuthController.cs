using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webapi_EFCore.Data;
using Webapi_EFCore.DTOs;
using Webapi_EFCore.Models;
using Webapi_EFCore.Services;
using Webapi_EFCore.Services.Interfaces;

namespace Webapi_EFCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtTokenService _jwt;
        private readonly ApplicationDbContext _context;
        public AuthController(IUserService userService, JwtTokenService jwt, ApplicationDbContext context)
        {
            _userService = userService;
            _jwt = jwt;
            _context = context;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.AuthenticateAsync(loginRequest.Username, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var accessToken = _jwt.GenerateAccessToken(user);
            var refreshToken = await _jwt.GenerateAndSaveRefreshTokenAsync(user.Username);

            return Ok(new JwtResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("config")]
        public IActionResult Config()
        {
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest req)
        {
            var isValid = await _jwt.ValidateRefreshTokenAsync(req.RefreshToken, req.Username);
            if (!isValid)
                return Unauthorized("Invalid refresh token");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username);
            if (user == null) return Unauthorized("not found user");

            var newAccessToken = _jwt.GenerateAccessToken(user);
            var newRefreshToken = await _jwt.GenerateAndSaveRefreshTokenAsync(user.Username);

            await _jwt.RevokeTokenAsync(req.RefreshToken);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest req)
        {
            await _jwt.RevokeTokenAsync(req.RefreshToken);
            return Ok(new { Message = "Logged out" });
        }
    }
}
