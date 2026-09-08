using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Google.Apis.Auth;
using System.Diagnostics.CodeAnalysis;
using AuthAPI.Security;
using Microsoft.AspNetCore.Identity;
namespace AuthAPI.Services
{
    public class AuthService : IAuthServcies 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpCilentFactory;
        public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IHttpClientFactory httpCilentFactory)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpCilentFactory = httpCilentFactory;
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
                Role = Roles.User
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
            var plainRefreshToken = CreateRefreshToken();
            user.RefreshToken = HashRefreshToken(plainRefreshToken);
            user.ExpiryTime = DateTime.Now.AddDays(7);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return (new TokenDto { AccessToken = token, RefreshToken = plainRefreshToken }, string.Empty);
        }

        // Cấp Refresh Token
        public async Task<(TokenDto? tokens, string error)> RefreshTokenAsync(TokenDto request)
        {
            var hashedInputToken = HashRefreshToken(request.RefreshToken); ;
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedInputToken);
            if (user == null) return (null, "Token không tồn tại.");
            if (user.ExpiryTime < DateTime.Now) return (null, "Token đã hết hạn.");
            var newAccessToken = CreateToken(user);
            var newPlainRefreshToken = CreateRefreshToken();
            user.RefreshToken = HashRefreshToken(newPlainRefreshToken);
            user.ExpiryTime = DateTime.Now.AddDays(7);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return (new TokenDto { AccessToken = newAccessToken, RefreshToken = newPlainRefreshToken }, string.Empty);
        }

        // Tạo Token
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? AuthAPI.Security.Roles.User)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
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

        // Login bằng các nền tảng
        public async Task<(TokenDto? tokens, string error)> ExternalLoginAsync(ExternalAuthDto request)
        {
            try
            {
                string email = string.Empty;
                if (request.Provider.Equals("Google", StringComparison.OrdinalIgnoreCase))
                {
                    email = await ValidateGoogleTokenAsync(request.Token);
                }
                else if (request.Provider.Equals("Facebook", StringComparison.OrdinalIgnoreCase))
                {
                    email = await ValidateFacebookTokenAsync(request.Token);
                }
                else
                {
                    return (null, "Không được hỗ trợ");
                }
                if (string.IsNullOrEmpty(email)) return (null, "Token không hợp lệ");
                return await ProcessExternalUserAsyncs(email);
            }
            catch (Exception ex)
            {
                string errorMsg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return (null, "Lỗi xác thực" + errorMsg);
            }
        }

//---------------------------- Hàm bổ trợ -----------------------------//

        // Google
        private async Task<string> ValidateGoogleTokenAsync(string token)
        {
            var clientId = _configuration["Google:ClientID"];
            if (string.IsNullOrEmpty(clientId))
            {
                clientId = _configuration["GoogleAuthSettings:ClientId"];
            }
            if (string.IsNullOrEmpty(clientId)) throw new Exception("Chưa cấu hinhd");
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { clientId },
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload.Email;
        }

        //Facebook
        private async Task<string> ValidateFacebookTokenAsync(string token)
        {
            var httpClient = _httpCilentFactory.CreateClient();
            var fbUrl = $"https://graph.facebook.com/me?fields=id,email,name&access_token={token}";
            var reponse = await httpClient.GetAsync(fbUrl);
            if (!reponse.IsSuccessStatusCode) return string.Empty;
            var content = await reponse.Content.ReadAsStringAsync();
            var fbData = JsonSerializer.Deserialize<JsonElement>(content);
            return fbData.TryGetProperty("email", out var emailProp) ? emailProp.GetString(): $"{fbData.GetProperty("id").GetString()}@facebook.com";
        }

        // Xử lý DB 
        private async Task<(TokenDto? tokens, string error)> ProcessExternalUserAsyncs(string email)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                user = new User
                {
                    Email = email,
                    Username = email,
                    Password = string.Empty,
                    Role = Roles.User,
                    ExpiryTime = DateTime.UtcNow
                };
                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }
            string accessToken = CreateToken(user);
            string plainRefreshToken = CreateRefreshToken();
            user.RefreshToken = HashRefreshToken(plainRefreshToken);
            user.ExpiryTime = DateTime.Now.AddDays(7);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.CompleteAsync();
            return (new TokenDto { AccessToken = accessToken, RefreshToken = plainRefreshToken }, string.Empty);
        }


        // Phân Role
        public async Task<string> AssignRoleAsync(string userEmail,  string newRole)
        {
            var validRoles = new List<string> { AuthAPI.Security.Roles.User, AuthAPI.Security.Roles.Captain, AuthAPI.Security.Roles.Admin, AuthAPI.Security.Roles.Referee };
            if (!validRoles.Contains(newRole))
            {
                return "Không hợp lệ";
            }
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null) return "Không tìm thấy người dùng";
            user.Role = newRole;
            _unitOfWork.Users.Update(user);
            await _unitOfWork .CompleteAsync();
            return "Thành Công";
        }

        // Thu hồi Tokens
        public async Task<bool> RevokeTokenAsync(string username)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;
            user.RefreshToken = null;
            _unitOfWork.Users.Update(user); ;
            await _unitOfWork.CompleteAsync();
            return true;
        }

        // Hash RefreshToken
        private string HashRefreshToken(string token)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}