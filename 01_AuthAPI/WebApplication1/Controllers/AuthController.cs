using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthAPI.Filters;
using AuthAPI.Models.DTO;
using AuthAPI.Services.Interfaces;
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDto request)
        {
            var (tokens, error) = await authService.RefreshTokenAsync(request);
            return tokens != null ? Ok(tokens) : BadRequest(error);
        }

        [HttpGet("profile"), Authorize]
        public IActionResult Profile() => Ok("Xác thực thành công");

        [HttpGet("admin-only"), Authorize(Roles = "Admin")]
        public IActionResult OnlyAdminEndpoint() => Ok(new { message = "Bạn đã vào quyền Admin" });
    }
}