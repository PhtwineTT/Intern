using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Auth;

namespace AuthAPI.Services.Interfaces
{
    public interface IVenueServices
    {
        Task<IEnumerable<VenueDto>> GetAllVenuesAsync(QueryParameters queryParams);
        Task<VenueDto?> GetVenueByIdAsync(int id);
        Task<string> CreateVenueAsync(CreateVenueDto request);
        Task<bool> UpdateVenueAsync(int id, CreateVenueDto request);
        Task<bool> DeleteVenueAsync(int id);
    }
}
