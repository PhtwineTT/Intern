using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;

namespace AuthAPI.Services.Interfaces
{
    public interface IRewardServices
    {
        Task<IEnumerable<RewardReponseDto>> GetAllRewardsAsync(QueryParameters queryParams);
        Task<RewardReponseDto?> GetRewardByIdAsync(int id);
        Task<string> CreateRewardAsync(RewardUpserDto request);
        Task<bool> UpdateRewardAsync(int id, RewardUpserDto request);
        Task<bool> DeleteRewardAsync(int id);
    }
}