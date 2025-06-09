using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webapi_EFCore.DTOs;
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
        public AuthController(IUserService userService, JwtTokenService jwt)
        {
            _userService = userService;
            _jwt = jwt;
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
            var refreshToken = _jwt.GenerateRefreshToken(user.Username);

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

        //[HttpPost("refresh")]
        //public async Task<IActionResult> Refresh([FromBody] JwtResponse tokenRequest)
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    var token = handler.ReadJwtToken(tokenRequest.AccessToken);
        //    var username = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        //    if (username is null || !_jwt.ValidateRefreshToken(username, tokenRequest.RefreshToken))
        //    {
        //        return Unauthorized("Invalid refresh token");
        //    }

        //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        //    if (user == null) return Unauthorized();

        //    var newAccessToken = _tokenService.GenerateAccessToken(user);
        //    var newRefreshToken = _tokenService.GenerateRefreshToken(user.Username);

        //    return Ok(new AuthResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        //}
    }
}
