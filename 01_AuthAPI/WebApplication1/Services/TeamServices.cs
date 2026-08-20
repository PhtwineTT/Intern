using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class TeamServices : ITeamServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public TeamServices(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task <IEnumerable<TeamResponseDto>> GetAllTeamsAsync(QueryParameters queryParams)
        {
            Expression<Func<Team, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.TeamName.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var teams = await _unitOfWork.Teams.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter,
                include: q => q.Include(t => t.Members)
                );
            return teams.Select(t => t.ToResponseDto());
        }
        public async Task<TeamResponseDto?> GetTeamByIdAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id, include: q => q.Include(t => t.Members));
            if (team == null) return null;
            return team.ToResponseDto();
        }
        public async Task <string> CreateTeamAsync(int currentUserID, TeamUpserDto request)
        {
            var team = request.ToEntity();
            team.CaptainId = currentUserID;
            await _unitOfWork.Teams.AddAsync(team);
            var user = await _unitOfWork .Users.GetByIdAsync(currentUserID);
            if (user != null && user.Role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                user.Role = "Captain";
                _unitOfWork.Users.Update(user);
            }
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }
        public async Task<bool> UpdateTeamAsync(int id, TeamUpserDto request)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null) return false;
            request.UpdateEntity(team);
            _unitOfWork.Teams.Update(team);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null) return false;
            _unitOfWork.Teams.Delete(team);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> UpdateTeamLogoAsync(int teamId, string imageUrl)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(teamId);
            if (team == null) return false;
            team.LogoURL = imageUrl;
            _unitOfWork.Teams.Update(team);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
