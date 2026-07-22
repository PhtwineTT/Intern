using AuthAPI.Models.DTO;

namespace AuthAPI.Services.Interfaces
{
    public interface IClubEventServices
    {
        Task<IEnumerable<ClubEventDto>> GetAllEventsAsync();
        Task<ClubEventDto?> GetEventByIdAsync(int id);
        Task<string> CreateEventAsync(ClubEventDto request);
        Task<string> UpdateEventAsync(int id, ClubEventDto request);
        Task<string> DeleteEventAsync(int id);
    }
}