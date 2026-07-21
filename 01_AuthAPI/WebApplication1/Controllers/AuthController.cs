using Microsoft.AspNetCore.Mvc;
using AuthAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using AuthAPI.Services;
using AuthAPI.Filters;
namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    // Tự động xác thực 
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Dependency Injection qua interface (IServices)
        private readonly IServices _authService;
        public AuthController(IServices authService)
        {
            _authService = authService;
        }

        // API đăng kí, dùng rate limit để giới hạn
        [HttpPost("register")]
        [RateLimit(maxRequest: 3, timeLimit: 10)]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result != "Success") return BadRequest(result);

            return Ok("Đăng ký thành công");
        }

        // API đăng nhập, dùng rate limit để giới hạn
        [HttpPost("login")]
        [RateLimit(maxRequest: 5, timeLimit: 60)]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var (tokens, error) = await _authService.LoginAsync(request);
            if (tokens == null) return BadRequest(error);

            return Ok(tokens);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenDto request)
        {
            var (tokens, error) = await _authService.RefreshTokenAsync(request);
            if (tokens == null) return BadRequest(error);

            return Ok(tokens);
        }

        // API xác thực người dùng qua token
        [HttpGet("profile")]
        [Authorize]
        public IActionResult Profile()
        {
            return Ok("Xác thực thành công");
        }

        // Phân quyền
        [HttpGet("admin-only")]
        [Authorize(Roles = "Admin")]
        public IActionResult OnlyAdminEndpoint()
        {
            return Ok(new { message = "Bạn đã vào quyền Admin" });
        }
    }
}