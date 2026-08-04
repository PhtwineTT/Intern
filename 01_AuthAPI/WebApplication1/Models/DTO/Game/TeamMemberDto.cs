namespace AuthAPI.Models.DTO.Game
{
    public class CreateTeamMemberDto
    {
        public int TeamId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string InGameName { get; set; } = string.Empty;
    }
    public class TeamMemberDto : CreateTeamMemberDto
    {
        public int Id { get; set; }
    }
}