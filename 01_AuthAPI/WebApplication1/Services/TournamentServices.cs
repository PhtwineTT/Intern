using AuthAPi.Services.Interface;
using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class TournamentServices : ITournamentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public TournamentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TournamentResponseDto>> GetAllTournamentAsync(QueryParameters queryParams)
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
                include: q => q.Include(t => t.Venue).Include(t => t.Teams)
            );
            return tournaments.Select(t => t.ToResponseDto());
        }
        public async Task<TournamentResponseDto?> GetTournamentByIdAsync(int id)
        {
            var tournament = await _unitOfWork.Tournaments.FirstOrDefaultAsync(
                t => t.Id == id,
                include: q => q.Include(t => t.Venue).Include(t => t.Teams));
            if (tournament == null) return  null;
            return tournament.ToResponseDto();
        }
        public async Task<string> CreateTournamentAsync(TournamentUpserDto request)
        {
            var tournament = request.ToEntity();
            await _unitOfWork.Tournaments.AddAsync(tournament);
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }
        public async Task<bool> UpdateTournamentAsync(int id, TournamentUpserDto request)
        {
            var tournament = await _unitOfWork.Tournaments.GetByIdAsync(id);
            if (tournament == null) return false;
            request.UpdateEntity(tournament);
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
        public async Task<string> RegisterTeamAsync(int tournamentId, RegisterTournamentDto request, int currentUserId)
        {
            var tournaments = await _unitOfWork.Tournaments.GetByIdAsync(tournamentId);
            if (tournaments == null) return "Không tồn tại";
            var team = await _unitOfWork.Teams.GetByIdAsync(request.TeamId);
            if (team == null) return "Không tồn tại";
            if (team.CaptainId != currentUserId) return "Không đủ quyền";
            var existingResgistration = await _unitOfWork.TournamentRegistrations.FirstOrDefaultAsync(tr => tr.TournamentId == tournamentId && tr.TeamId == request.TeamId);
            if (existingResgistration != null) return "Chỉ được nộp một lần";
            var registration = new TournamentRegistration
            {
                TournamentId = tournamentId,
                TeamId = request.TeamId,
                Status = "Pending",
                RegistereAt = DateTime.UtcNow,
            };
            await _unitOfWork.TournamentRegistrations.AddAsync(registration);
            await _unitOfWork.CompleteAsync();
            return "Thành công";
        }
        public async Task<string> UpdateRegistrationStatusAsync(int registrationId, UpdateRegistrationStatusDto request)
        {
            var registration = await _unitOfWork.TournamentRegistrations.GetByIdAsync(registrationId);
            if (registration == null) return "Không tồn tại";
            registration.Status = request.Status;
            _unitOfWork.TournamentRegistrations.Update(registration);
            if (request.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase)){
                var tournament = await _unitOfWork.Tournaments.GetByIdAsync(
                    registration.TournamentId,
                    include: q => q.Include(t => t.Teams)
                    );
                var team = await _unitOfWork.Teams.GetByIdAsync(registration.TeamId);
                if (team != null && tournament != null && !tournament.Teams.Any(t => t.Id == team.Id))
                {
                    tournament.Teams.Add(team);
                }
            }
            await _unitOfWork.CompleteAsync();
            return "Đã cập nhật";
        }
    }
}   