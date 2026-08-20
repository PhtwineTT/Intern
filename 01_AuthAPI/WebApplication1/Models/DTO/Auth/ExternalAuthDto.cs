using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO.Auth
{
    public class ExternalAuthDto
    {
        [Required(ErrorMessage = "Tên nền tảng không được trống")]
        public string Provider { get; set; } = string.Empty;
        [Required(ErrorMessage = "Token không được trống")]
        public string Token { get; set; } = string.Empty;
    }
}
