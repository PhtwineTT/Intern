using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AuthAPI.Models.DTO.Game
{
    public class TeamUpserDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "Id Giải đấu không được trống")]
        public int TournamentId { get; set; }
    }
    public class TeamResponseDto
    {
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string LogoURL { get; set; } = string.Empty;
        public int CaptainId { get; set; }
        public int TotalMembers { get; set; }
    } 
    public static class TeamMapping
    {
        public static Team ToEntity(this TeamUpserDto dto)
        {
            return new Team
            {
                TeamName = dto.TeamName,
                LogoURL = dto.LogoURL,
            };
        }
        public static void UpdateEntity (this TeamUpserDto dto, Team team)
        {
            team.TeamName = dto.TeamName;
            if (!string.IsNullOrEmpty(dto.LogoURL))
            {
                team.LogoURL = dto.LogoURL;
            }
        }
        public static TeamResponseDto ToResponseDto(this Team team)
        {
            return new TeamResponseDto
            {
                Id = team.Id,
                TeamName = team.TeamName,
                LogoURL = team.LogoURL,
                CaptainId = team.CaptainId,
                TotalMembers = team.Members?.Count ?? 0,
            };
        }
    }
}