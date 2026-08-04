namespace AuthAPI.Models.DTO.Game
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        public string CaptainId { get; set; } = string.Empty;
        public int TournamentId { get; set; }
        public string? TournamentName { get; set; }
    }
}