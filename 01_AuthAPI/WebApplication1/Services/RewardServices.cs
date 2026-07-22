using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Repositories.Interfaces;
using AuthAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services
{
    public class RewardServices(IUnitOfWork unitOfWork) : IRewardServices
    {
        public async Task<IEnumerable<RewardDto>> GetAllRewardAsync()
        {
            var rewards = await unitOfWork.Rewards.GetAllAsync();
            return rewards.Select(r => new RewardDto
            {
                Id = r.Id,
                ItemName = r.ItemName,
                StockQuantity = r.StockQuantity,
                PointsRequired = r.PointsRequired
            });
        }
        public async Task<RewardDto?> GetRewardByIdAsync(int id)
        {
            var r = await unitOfWork.Rewards.GetByIdAsync(id);
            if (r == null) return null;

            return new RewardDto
            {
                Id = r.Id,
                ItemName = r.ItemName,
                StockQuantity = r.StockQuantity,
                PointsRequired = r.PointsRequired
            };
        }
        public async Task<string> AddRewardAsync(RewardDto request)
        {
            try
            {
                var newReward = new Reward
                {
                    ItemName = request.ItemName,
                    StockQuantity = request.StockQuantity,
                    PointsRequired = request.PointsRequired
                };

                await unitOfWork.Rewards.AddAsync(newReward);
                await unitOfWork.CompleteAsync();
                return "Success";
            }
            catch (DbUpdateException)
            {
                return "Lỗi khi lưu phần thưởng. Dữ liệu vi phạm ràng buộc dưới Database.";
            }
        }
        public async Task<string> UpdateRewardAsync(int id, RewardDto request)
        {
            var existing = await unitOfWork.Rewards.GetByIdAsync(id);
            if (existing == null) return "Không tìm thấy phần thưởng";

            try
            {
                existing.ItemName = request.ItemName;
                existing.StockQuantity = request.StockQuantity;
                existing.PointsRequired = request.PointsRequired;

                unitOfWork.Rewards.Update(existing);
                await unitOfWork.CompleteAsync();
                return "Success";
            }
            catch (DbUpdateException)
            {
                return "Lỗi khi cập nhật. Dữ liệu vi phạm ràng buộc dưới Database.";
            }
        }
        public async Task<string> DeleteRewardAsync(int id)
        {
            var existing = await unitOfWork.Rewards.GetByIdAsync(id);
            if (existing == null) return "Không tìm thấy phần thưởng";

            unitOfWork.Rewards.Delete(existing);
            await unitOfWork.CompleteAsync();
            return "Success";
        }
    }
}