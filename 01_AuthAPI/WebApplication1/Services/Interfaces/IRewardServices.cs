using AuthAPI.Models.DTO;

namespace AuthAPI.Services.Interfaces
{
    public interface IRewardServices
    {
        Task<IEnumerable<RewardDto>> GetAllRewardAsync();
        Task<RewardDto?> GetRewardByIdAsync(int id);
        Task<string> AddRewardAsync(RewardDto request);
        Task<string> UpdateRewardAsync(int id, RewardDto request);
        Task<string> DeleteRewardAsync(int id);
    }
}