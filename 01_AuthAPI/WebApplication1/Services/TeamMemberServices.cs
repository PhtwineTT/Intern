using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace AuthAPI.Services
{
    public class TeamMemberServices : ITeamMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TeamMemberServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TeamMemberResponseDto>> GetAllMembersAsync(QueryParameters queryParams)
        {
            Expression<Func<TeamMember, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = tm => tm.InGameName.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var teamMembers = await _unitOfWork.TeamMembers.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter,
                include: q => q.Include(tm => tm.Team)
            );
            return teamMembers.Select(tm => tm.ToResponseDto());
        }
        public async Task<TeamMemberResponseDto?> GetMemberByIdAsync(int id)
        {
            var teamMember = await _unitOfWork.TeamMembers.FirstOrDefaultAsync(
                tm => tm.Id == id,
                include: q => q.Include(tm => tm.Team)
                );
            if (teamMember == null) return null;
            return teamMember.ToResponseDto();
        }
        public async Task<string> CreateMemberAsync(TeamMemberUpserDto request)
        {
            var TeamExists = await _unitOfWork.Teams.GetByIdAsync(request.TeamId);
            if (TeamExists == null)
            {
                return "Đội không tồn tại";
            }
            var member = _mapper.Map<TeamMember>(request);
            await _unitOfWork.TeamMembers.AddAsync(member);
            await _unitOfWork.CompleteAsync();
            return "Cập nhật thành công";
        }
        public async Task<bool> UpdateMemberAsync(int id, TeamMemberUpserDto request)
        {
            var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
            if (member == null) return false;
            if (member.TeamId != request.TeamId)
            {
                var teamExists = await _unitOfWork.Teams.GetByIdAsync(request.TeamId);
                if (teamExists == null) return false;
            }
            request.UpdateEntity(member);
            _unitOfWork.TeamMembers.Update(member);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteMemberAsync(int id)
        {
            var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
            if (member == null) return false;
            _unitOfWork.TeamMembers.Delete(member);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
