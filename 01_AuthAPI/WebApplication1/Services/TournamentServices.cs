using AuthAPi.Services.Interface;
using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class TournamentServices : ITournamentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TournamentServices (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TournamentDto>> GetAllTournamentAsync(QueryParameters queryParams)
        {
            Expression<Func<Tournament, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.Name.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var tournaments = await _unitOfWork.Tournaments.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter,
                includeProperties: "Venue, teams"
            );
            return _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
        }
        public async Task<TournamentDto?> GetTournamentByIdAsync(int id)
        {
            var tournament = await _unitOfWork.Tournaments.FirstOrDefaultAsync(
                t => t.Id == id,
                includeProperties: "Venue,Teams");
            if (tournament == null) return  null;
            return _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<string> CreateTournamentAsync(CreateTournamentDto request)
        {
            var tournament = _mapper.Map<Tournament>(request);
            await _unitOfWork.Tournaments.AddAsync(tournament);
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }
        public async Task<bool> UpdateTournamentAsync(int id, CreateTournamentDto request)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
            if (tournament == null) return false;
            _mapper.Map(request, tournament);
            _unitOfWork.Tournaments.Update(tournament);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteTournamentAsync(int id)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
            if (tournament == null) return false;
            _unitOfWork.Tournaments.Delete(tournament);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}