using AuthAPI.DATA;
using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthAPI.Services
{
    public class AuthService : IAuthServcies 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        // Đăng ký
        public async Task<string> RegisterAsync(RegisterDto request)
        {
            bool checkUpper = await _unitOfWork.Users.AnyAsync(u => EF.Functions.Collate(u.Username, "SQL_Latin1_General_CP1_CS_AS") == request.Username);
            if (checkUpper) return "Tên tài khoản đã tồn tại";
            if (await _unitOfWork.Users.AnyAsync(u => u.Email == request.Email))
            {
                return "Email đã được sử dụng";
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User()
            {
                Username = request.Username,
                Password = passwordHash,
                Email = request.Email,
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return "Success";
        }

        // Đăng nhập và cấp Token
        public async Task<(TokenDto? tokens, string error)> LoginAsync(LoginDto request)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => EF.Functions.Collate(u.Username, "SQL_Latin1_General_CP1_CS_AS") == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return (null, "Sai tài khoản hoặc mật khẩu");
            }
            var token = CreateToken(user);
            var refreshToken = CreateRefreshToken();
            user.RefreshToken = refreshToken;
            user.ExpiryTime = DateTime.Now.AddDays(7);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return (new TokenDto { AccessToken = token, RefreshToken = refreshToken }, string.Empty);
        }

        // Cấp Refresh Token
        public async Task<(TokenDto? tokens, string error)> RefreshTokenAsync(TokenDto request)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
            if (user == null) return (null, "Token không tồn tại.");
            if (user.ExpiryTime < DateTime.Now) return (null, "Token đã hết hạn.");
            var newAccessToken = CreateToken(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.ExpiryTime = DateTime.Now.AddDays(7);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return (new TokenDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken }, string.Empty);
        }

        // Tạo Token
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "Member")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Tạo Refresh Token
        private string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}