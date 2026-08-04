using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using AutoMapper;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class TeamServices : ITeamServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeamServices(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task <IEnumerable<TeamDto>> GetAllTeamsAsync(QueryParameters queryParams)
        {
            Expression<Func<Team, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.TeamName.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var teams = await _unitOfWork.Teams.GetPagedAsync(
                pageNumber: queryParams.PageSize,
                pageSize: queryParams.PageSize,
                filter: filter,
                includeProperties: "Tournament, TeamMember"
                );
            return _mapper.Map<IEnumerable<TeamDto>>( teams );
        }
        public async Task<TeamDto?> GetTeamByIdAsync(int id)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null) return null;
            return _mapper.Map<TeamDto>(team);
        }
        public async Task <string> CreateTeamAsync(CreateTeamDto request)
        {
            var team = _mapper.Map<Team>(request);
            await _unitOfWork.Teams.AddAsync(team);
            await _unitOfWork.CompleteAsync();
            return "Tạo thành công";
        }
        public async Task<bool> UpdateTeamAsync(int id, CreateTeamDto request)
        {
            var team = await _unitOfWork.Teams.GetByIdAsync(id);
            if (team == null) return false;
            _mapper.Map(request, team);
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
