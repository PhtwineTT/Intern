using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
namespace AuthAPi.Services.Interface
{
    public interface ITournamentServices
    {
        Task<IEnumerable<TournamentDto>> GetAllTournamentAsync(QueryParameters queryParams);
        Task<TournamentDto?> GetTournamentByIdAsync(int id);
        Task<string> CreateTournamentAsync(CreateTournamentDto request);
        Task<bool> UpdateTournamentAsync(int id, CreateTournamentDto request);
        Task<bool> DeleteTournamentAsync(int id);
    }
}