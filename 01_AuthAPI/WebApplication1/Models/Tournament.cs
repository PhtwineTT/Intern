using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int MaxTeams { get; set; }
        [Precision (18,2)]
        public decimal PrizePool { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.Upcoming;
        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
        public ICollection<TournamentRegistration> Registrations { get; set; } = new List<TournamentRegistration>();
    }
    public enum TournamentStatus
    {
        Upcoming = 0,
        Ongoing = 1,    
        Completed = 2,
        Canceled = 3,
    }
}
