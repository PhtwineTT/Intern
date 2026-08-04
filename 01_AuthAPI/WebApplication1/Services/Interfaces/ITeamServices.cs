using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
namespace AuthAPI.Services.Interfaces
{
    public interface ITeamServices
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync(QueryParameters queryParams); 
        Task<TeamDto?> GetTeamByIdAsync(int id);
        Task<string> CreateTeamAsync(CreateTeamDto request);
        Task<bool> UpdateTeamAsync(int id, CreateTeamDto request);
        Task<bool> DeleteTeamAsync(int id);
        Task<bool> UpdateTeamLogoAsync(int teamId, string imageUrl);
    }
}
