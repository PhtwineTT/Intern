using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
namespace AuthAPi.Services.Interface
{
    public interface ITournamentServices
    {
        Task<IEnumerable<TournamentResponseDto>> GetAllTournamentAsync(QueryParameters queryParams);
        Task<TournamentResponseDto?> GetTournamentByIdAsync(int id);
        Task<string> CreateTournamentAsync(TournamentUpserDto request);
        Task<bool> UpdateTournamentAsync(int id, TournamentUpserDto request);
        Task<bool> DeleteTournamentAsync(int id);
        Task<string> RegisterTeamAsync(int tournamentId, RegisterTournamentDto request, int currentUserId);
        Task<string> UpdateRegistrationStatusAsync(int registrationId, UpdateRegistrationStatusDto request);
    }
}