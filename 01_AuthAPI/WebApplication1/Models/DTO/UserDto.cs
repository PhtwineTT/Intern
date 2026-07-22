using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string Role { get; set; } = "User";
        public string Email { get; set; } = string.Empty;
    }
}
