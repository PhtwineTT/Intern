using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using AutoMapper;
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
        public async Task <IEnumerable<TeamMemberDto>> GetAllMembersAsync(QueryParameters queryParams)
        {
            Expression<Func<TeamMember, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.InGameName.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var members = await _unitOfWork.TeamMembers.GetPagedAsync(
                pageNumber: queryParams.PageSize,
                pageSize: queryParams.PageSize,
                filter: filter,
                includeProperties: "Team");
            return _mapper.Map<IEnumerable<TeamMemberDto>>(members);
        }
        public async Task<TeamMemberDto?> GetMemberByIdAsync(int id)
        {
            var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
            if (member == null) return null;
            return _mapper.Map<TeamMemberDto>(member);
        }
        public async Task<string> CreateMemberAsync(CreateTeamMemberDto request)
        {
            var member = _mapper.Map<TeamMember>(request);
            await _unitOfWork.TeamMembers.AddAsync(member);
            await _unitOfWork.CompleteAsync();
            return "Cập nhật thành công";
        }
        public async Task<bool> UpdateMemberAsync(int id, CreateTeamMemberDto request)
        {
            var member = await _unitOfWork.TeamMembers.GetByIdAsync(id);
            if (member == null) return false;
            _mapper.Map(request, member);
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
