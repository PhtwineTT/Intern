using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using AutoMapper;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class VenueServices : IVenueServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VenueServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync(QueryParameters queryParams)
        {
            Expression<Func<Venue, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.Name.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var venues = await _unitOfWork.Venues.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter,
                includeProperties: "Tournament"
                );
            return _mapper.Map<IEnumerable<VenueDto>>(venues); 
        }
        public async Task<VenueDto?> GetVenueByIdAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);
            if (venue == null) return null;
            return _mapper.Map<VenueDto>(venue);
        }
        public async Task<string> CreateVenueAsync(CreateVenueDto request)
        {
            var venue = _mapper.Map<Venue>(request);
            await _unitOfWork.Venues.AddAsync(venue);
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }
        public async Task<bool> UpdateVenueAsync(int id, CreateVenueDto request)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);
            if (venue == null) return false;
            _mapper.Map(request, venue);
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
