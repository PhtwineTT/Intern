namespace AuthAPI.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string InGameName { get; set; } = string.Empty;
        public Team? Team { get; set; }
    }
}
