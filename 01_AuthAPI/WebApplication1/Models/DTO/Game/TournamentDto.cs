namespace AuthAPI.Models.DTO.Game
{
    public class VenueBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class TeamBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class TournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GameTitle { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int MaxTeams { get; set; }
        public decimal PrizePool { get; set; }
        public string Status { get; set; } = string.Empty;
        public VenueBasicDto? Venue { get; set; }
        public List<TeamBasicDto> Teams { get; set; } = new List<TeamBasicDto>();

    }
}
