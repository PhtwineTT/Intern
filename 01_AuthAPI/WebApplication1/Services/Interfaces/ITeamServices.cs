using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
namespace AuthAPI.Services.Interfaces
{
    public interface ITeamServices
    {
        Task<IEnumerable<TeamResponseDto>> GetAllTeamsAsync(QueryParameters queryParams); 
        Task<TeamResponseDto?> GetTeamByIdAsync(int id);
        Task<string> CreateTeamAsync(int currentUserId, TeamUpserDto request);
        Task<bool> UpdateTeamAsync(int id, TeamUpserDto request);
        Task<bool> DeleteTeamAsync(int id);
        Task<bool> UpdateTeamLogoAsync(int teamId, string imageUrl);
    }
}
