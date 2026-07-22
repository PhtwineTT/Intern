using AuthAPI.Models.DTO;
namespace AuthAPI.Services.Interfaces
{
    public interface IAuthServcies
    {
        Task<string> RegisterAsync(RegisterDto request);
        Task<(TokenDto? tokens, string error)> LoginAsync(LoginDto request);
        Task<(TokenDto? tokens, string error)> RefreshTokenAsync(TokenDto request);
    }
}
