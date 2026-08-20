namespace AuthAPI.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        public int CaptainId { get; set; }
        public User? Captain { get; set; }
        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
        public ICollection<TournamentRegistration> Registration { get; set; } = new List<TournamentRegistration>();
    }
}
