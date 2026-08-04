using System.ComponentModel.DataAnnotations;
namespace AuthAPI.Models.DTO.Game
{
    public class CreateTournamentDto
    {
        [Required(ErrorMessage = " Tên không được để trống")] 
        public string Name { get; set; } = string.Empty;
        [Required]
        public GameTitle GameTitle { get; set; }
        public string Format { get; set; } = string.Empty;
        [Range(8, 128, ErrorMessage = "Số đội phải từ 8 đến 128")]
        public int MaxTeams { get; set; }
        public decimal PrizePool { get; set; }
        public int? VenueId { get; set; }
    }
}
