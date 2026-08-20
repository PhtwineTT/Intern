using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Auth;

namespace AuthAPI.Services.Interfaces
{
    public interface IVenueServices
    {
        Task<IEnumerable<VenueResponseDto>> GetAllVenuesAsync(QueryParameters queryParams);
        Task<VenueResponseDto?> GetVenueByIdAsync(int id);
        Task<string> CreateVenueAsync(VenueUpserDto request);
        Task<bool> UpdateVenueAsync(int id, VenueUpserDto request);
        Task<bool> DeleteVenueAsync(int id);
    }
}
