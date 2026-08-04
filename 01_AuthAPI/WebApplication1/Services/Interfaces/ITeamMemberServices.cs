using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;

namespace AuthAPI.Services.Interfaces
{
    public interface ITeamMemberServices
    {
        Task<IEnumerable<TeamMemberDto>> GetAllMembersAsync(QueryParameters queryParams);
        Task<TeamMemberDto?> GetMemberByIdAsync(int id);
        Task<string> CreateMemberAsync(CreateTeamMemberDto request);
        Task<bool> UpdateMemberAsync(int id, CreateTeamMemberDto request);
        Task<bool> DeleteMemberAsync(int id);
    }
}
