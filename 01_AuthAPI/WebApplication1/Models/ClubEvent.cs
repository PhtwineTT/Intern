namespace AuthAPI.Models
{
    public class ClubEvent
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public int MaxAttendes { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
