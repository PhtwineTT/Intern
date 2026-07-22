using AuthAPI.Models;
using AuthAPI.Models.DTO;
namespace AuthAPI.Services.Interfaces
{
    public interface IRoomListingServices
    {
        Task<IEnumerable<RoomListing>> GetAllRoomAsync();
        Task<RoomListing?> GetRoomByIdAsync(int id);
        Task<string> CreateRoomAsync(RoomListingDto request);
    }
}
