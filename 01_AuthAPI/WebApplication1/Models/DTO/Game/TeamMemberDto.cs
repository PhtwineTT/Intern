using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO.Game
{
    public class TeamMemberUpserDto
    {
        [Required(ErrorMessage = "Đội không được để trống")]
        public int TeamId { get; set; }
        [Required(ErrorMessage = "Người dùng không được để trống")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Tên Ingame không được để trống")]
        public string InGameName { get; set; } = string.Empty;
    }
    public class TeamMemberResponseDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public string InGameName { get; set; } = string.Empty;
        public string? TeamName { get; set; }
    }
    public static class TeamMemberMappingExtension
    {
        public static TeamMember ToEntity(this TeamMemberUpserDto dto)
        {
            return new TeamMember
            {
                TeamId = dto.TeamId,
                UserId = dto.UserId,
                InGameName = dto.InGameName,
            };
        }
        public static void UpdateEntity(this TeamMemberUpserDto dto, TeamMember teamMember)
        {
            teamMember.TeamId = dto.TeamId;
            teamMember.UserId = dto.UserId;
            teamMember.InGameName = dto.InGameName;
        }
        public static TeamMemberResponseDto ToResponseDto(this TeamMember teamMember)
        {
            return new TeamMemberResponseDto
            {
                Id = teamMember.Id,
                TeamId = teamMember.TeamId,
                UserId = teamMember.UserId,
                InGameName = teamMember.InGameName,
                TeamName = teamMember.Team?.TeamName
            };
        }
    }
}