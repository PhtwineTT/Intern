using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;

namespace AuthAPI.Services
{
    public class RoomListingServices(IUnitOfWork unitOfWork) : IRoomListingServices
    {
        public async Task<IEnumerable<RoomListing>> GetAllRoomAsync()
        {
            return await unitOfWork.RoomListing.GetAllAsync();
        }
        public async Task<RoomListing?> GetRoomByIdAsync(int id)
        {
            return await unitOfWork.RoomListing.GetByIdAsync(id);
        }
        public async Task<string> CreateRoomAsync(RoomListingDto request)
        {
            var newRoom = new RoomListing
            {
                Title = request.Title,
                Price = request.Price,
                Area = request.Area,
                Address = request.Address,
            };
            await unitOfWork.RoomListing.AddAsync(newRoom);
            await unitOfWork.CompleteAsync();
            return "Đã tạo phòng";
        }
    }
}
