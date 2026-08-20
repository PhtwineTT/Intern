using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace AuthAPI.Services
{
    public class VenueServices : IVenueServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public VenueServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VenueResponseDto>> GetAllVenuesAsync(QueryParameters queryParams)
        {
            Expression<Func<Venue, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.Name.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }

            var venues = await _unitOfWork.Venues.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter
            );
            return venues.Select(v => v.ToResponseDto());
        }

        public async Task<VenueResponseDto?> GetVenueByIdAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);
            if (venue == null) return null;
            return venue.ToResponseDto();
        }

        public async Task<string> CreateVenueAsync(VenueUpserDto request)
        {
            var venue = request.ToEntity();
            await _unitOfWork.Venues.AddAsync(venue);
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }

        public async Task<bool> UpdateVenueAsync(int id, VenueUpserDto request) 
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);
            if (venue == null) return false;
            request.UpdateEntity(venue);
            _unitOfWork.Venues.Update(venue);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteVenueAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);
            if (venue == null) return false;
            _unitOfWork.Venues.Delete(venue);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}