namespace AuthAPI.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public GameTitle GameTitle { get; set; }
        public string Format { get; set; } = string.Empty;
        public int MaxTeams { get; set; }
        public decimal PrizePool { get; set; }
        public TournamentStatus Status { get; set; }
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
