using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO.Game
{
    public class CreateTeamDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        [Required]
        public int TournamentId { get; set; }
    }
}
