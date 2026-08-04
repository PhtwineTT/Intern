namespace AuthAPI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        public string CaptainId { get; set; } = string.Empty;
        public int TournamentId { get; set; }
        public Tournament? Tournament { get; set; }
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}
