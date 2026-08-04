using AuthAPI.Models;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

namespace AuthAPI.Services
{
    public class RewardServices : IRewardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RewardServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RewardDto>> GetAllRewardsAsync(QueryParameters queryParams)
        {
            Expression<Func<Reward, bool>>? filter = null;
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                filter = t => t.ItemName.ToLower().Contains(queryParams.SearchTerm.ToLower());
            }
            var rewards = await _unitOfWork.Rewards.GetPagedAsync(
                pageNumber: queryParams.PageNumber,
                pageSize: queryParams.PageSize,
                filter: filter,
                includeProperties: ""
                );
            return _mapper.Map<IEnumerable<RewardDto>>(rewards);
        }
        public async Task<RewardDto?> GetRewardByIdAsync(int id)
        {
            var reward = await _unitOfWork.Rewards.GetByIdAsync(id);
            if (reward == null) return null;
            return _mapper.Map<RewardDto>(reward);
        }
        public async Task<string> CreateRewardAsync(CreateRewardDto request)
        {
            var reward = _mapper.Map<Reward>(request);
            await _unitOfWork.Rewards.AddAsync(reward);
            await _unitOfWork.CompleteAsync();
            return "Đã tạo";
        }
        public async Task<bool> UpdateRewardAsync(int id, CreateRewardDto request)
        {
            var reward = await _unitOfWork.Rewards.GetByIdAsync(id);
            if (reward == null) return false;
            _mapper.Map(request, reward);
            _unitOfWork.Rewards.Update(reward);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> DeleteRewardAsync(int id)
        {
            var reward = await _unitOfWork.Rewards.GetByIdAsync(id);
            if (reward == null) return false;

            _unitOfWork.Rewards.Delete(reward);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}