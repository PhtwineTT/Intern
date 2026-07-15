using System.ComponentModel.DataAnnotations;
namespace AuthAPI.DTO
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        [MinLength(5, ErrorMessage = "Tên tài khoản phải có ít nhất 5 ký tự")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Tên tài khoản chỉ được chứa chữ cái và số")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MinLength(8, ErrorMessage = "Mật khẩu phải dài ít nhất 8 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Mật khẩu phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string Password { get; set; } = string.Empty;
    }
}