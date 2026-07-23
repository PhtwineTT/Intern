using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services
{
    public class ClubEventServices(IUnitOfWork unitOfWork) : IClubEventServices
    {
        public async Task<IEnumerable<ClubEventDto>> GetAllEventsAsync()
        {
            var events = await unitOfWork.ClubEvents.GetAllAsync();
            return events.Select(e => new ClubEventDto
            {
                Id = e.Id,
                Name = e.Name,
                Theme = e.Theme,
                StartTime = e.StartTime,
                MaxAttendes = e.MaxAttendes,
                Location = e.Location,
            });
        }
        public async Task<ClubEventDto?> GetEventByIdAsync(int id)
        {
            var e = await unitOfWork.ClubEvents.GetByIdAsync(id);
            if (e == null) return null;
            return new ClubEventDto
            {
                Id = e.Id,
                Name = e.Name,
                Theme = e.Theme,
                StartTime = e.StartTime,
                MaxAttendes = e.MaxAttendes,
                Location = e.Location,
            };
        }
        public async Task<string> CreateEventAsync(ClubEventDto request)
        {
            try
            {
                var newEvent = new ClubEvent
                {
                    Name = request.Name,
                    Theme = request.Theme,
                    StartTime = request.StartTime,
                    MaxAttendes = request.MaxAttendes,
                    Location = request.Location,

                };
                await unitOfWork.ClubEvents.AddAsync(newEvent);
                await unitOfWork.CompleteAsync();
                return "Success";
            }
            catch (DbUpdateException)
            {
                return "Lỗi khi tạo sự kiện. Dữ liệu vi phạm ràng buộc DB.";
            }
        }
        public async Task<string> UpdateEventAsync(int id, ClubEventDto request)
        {
            var existingEvent = await unitOfWork.ClubEvents.GetByIdAsync(id);
            if (existingEvent == null) return "Không tìm thấy sự kiện";
            try
            {
                existingEvent.Name = request.Name;
                existingEvent.Theme = request.Theme;
                existingEvent.StartTime = request.StartTime;
                existingEvent.MaxAttendes = request.MaxAttendes;
                existingEvent.Location = request.Location;

                unitOfWork.ClubEvents.Update(existingEvent);
                await unitOfWork.CompleteAsync();
                return "Success";
            }
            catch (DbUpdateException)
            {
                return "Lỗi khi cập nhật sự kiện.";
            }
        }
        public async Task<string> DeleteEventAsync(int id)
        {
            var existingEvent = await unitOfWork.ClubEvents.GetByIdAsync(id);
            if (existingEvent == null) return "Không tìm thấy sự kiện";
            unitOfWork.ClubEvents.Delete(existingEvent);
            await unitOfWork.CompleteAsync();
            return "Success";
        }
    }
}