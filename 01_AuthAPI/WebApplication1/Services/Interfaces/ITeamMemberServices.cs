using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;

namespace AuthAPI.Services.Interfaces
{
    public interface ITeamMemberServices
    {
        Task<IEnumerable<TeamMemberResponseDto>> GetAllMembersAsync(QueryParameters queryParams);
        Task<TeamMemberResponseDto?> GetMemberByIdAsync(int id);
        Task<string> CreateMemberAsync(TeamMemberUpserDto request);
        Task<bool> UpdateMemberAsync(int id, TeamMemberUpserDto request);
        Task<bool> DeleteMemberAsync(int id);
    }
}
