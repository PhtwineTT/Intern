using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO.Game
{
    public class RegisterTournamentDto
    {
        [Required(ErrorMessage = "Vui lòng chọn Đội tuyển")]
        public int TeamId { get; set; }
    }
    public class UpdateRegistrationStatusDto
    {
        [Required(ErrorMessage = "Vui lòng cập nhật trạng thái")]
        [RegularExpression("^(Approved|Rejected)$", ErrorMessage = "Trạng thái chỉ có thể là Approved hoặc Rejected")]
        public string Status { get; set; } = string.Empty;
    }
}