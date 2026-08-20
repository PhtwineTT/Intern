using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AuthAPI.Models
{
    public class TournamentRegistration
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public Tournament? Tournament { get; set; }
        public int TeamId {  get; set; }
        public Team? Team { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime RegistereAt { get; set; } = DateTime.Now;
    }
}
