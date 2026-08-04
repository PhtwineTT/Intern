using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;

namespace AuthAPI.Services.Interfaces
{
    public interface IRewardServices
    {
        Task<IEnumerable<RewardDto>> GetAllRewardsAsync(QueryParameters queryParams);
        Task<RewardDto?> GetRewardByIdAsync(int id);
        Task<string> CreateRewardAsync(CreateRewardDto request);
        Task<bool> UpdateRewardAsync(int id, CreateRewardDto request);
        Task<bool> DeleteRewardAsync(int id);
    }
}