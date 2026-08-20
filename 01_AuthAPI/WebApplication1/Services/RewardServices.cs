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
    public class RewardServices : IRewardServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public RewardServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<RewardReponseDto>> GetAllRewardsAsync(QueryParameters queryParams)
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
                include: q => q.Include(r => r.Tournament)
                );
            return rewards.Select(r => r.ToReponseDto());
        }
        public async Task<RewardReponseDto?> GetRewardByIdAsync(int id)
        {
            var reward = await _unitOfWork.Rewards.GetByIdAsync(id);
            if (reward == null) return null;
            return reward.ToReponseDto();
        }
        public async Task<string> CreateRewardAsync(RewardUpserDto request)
        {
            var tournamentExists = await _unitOfWork.Tournaments.GetByIdAsync(request.TournamentId);
            if (tournamentExists == null)
            {
                return "Giải đấu không tồn tại";
            }
            var reward = request.ToEntity();
            await _unitOfWork.Rewards.AddAsync(reward);
            await _unitOfWork.CompleteAsync();
            return "Đã tạo";
        }
        public async Task<bool> UpdateRewardAsync(int id, RewardUpserDto request)
        {
            var reward = await _unitOfWork.Rewards.GetByIdAsync(id);
            if (reward == null) return false;
            if (reward.TournamentId != request.TournamentId)
            {
                var tournamentExists = await _unitOfWork.Tournaments.GetByIdAsync(request.TournamentId);
                if (tournamentExists == null) return false;
            }
            request.UpdateEntity(reward);
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