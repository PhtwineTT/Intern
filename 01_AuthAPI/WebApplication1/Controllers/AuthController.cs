using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthAPI.Security;
using AuthAPI.Filters;
using AuthAPI.Services.Interfaces;
using AuthAPI.Models.DTO.Auth;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata.Ecma335;
namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServcies authService) : ControllerBase
    {
        [HttpPost("register")]
        [RateLimit(maxRequest: 3, timeLimit: 10)]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result = await authService.RegisterAsync(request);
            return result == "Success" ? Ok("Đăng ký thành công") : BadRequest(result);
        }

        [HttpPost("login")]
        [RateLimit(maxRequest: 5, timeLimit: 60)]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var (tokens, error) = await authService.LoginAsync(request);
            return tokens != null ? Ok(tokens) : BadRequest(error);
        }

        [HttpPost("external-login")]
        [RateLimit(maxRequest: 5, timeLimit:60)]
        public async Task<IActionResult> ExternalLogin(ExternalAuthDto request)
        {
            var (tokens, error) = await authService.ExternalLoginAsync(request);
            return tokens != null ? Ok(tokens) : BadRequest(error);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDto request)
        {
            var (tokens, error) = await authService.RefreshTokenAsync(request);
            return tokens != null ? Ok(tokens) : BadRequest(error);
        }

        [HttpGet("profile"), Authorize]
        public IActionResult Profile() => Ok("Xác thực thành công");

        [HttpPost("assign-role")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto request)
        {
            var result = await authService.AssignRoleAsync(request.Email, request.Role);
            if (result == "Thành Công")
            {
                return Ok(new { message = "Đã cập nhật" });
            }
            return BadRequest(new {message = result});
        }

        [HttpPost("revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username)) return Unauthorized();
            var result = await authService.RevokeTokenAsync(username);
            if (!result) return BadRequest("Không thu hồi được");
            return Ok("Đã đăng xuất");
        }
    }
}