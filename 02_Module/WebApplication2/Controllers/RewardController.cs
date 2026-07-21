using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.Models;
using Module.DTOs;

namespace Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        public RewardControllers(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewReward([FromBody] RewardDto request)
        {
            var newReward = new Reward
            {
                ItemName = request.ItemName,
                StockQuantity = request.StockQuantity,
                PointsRequired = request.PointsRequired
            };
            _context.Rewards.Add(newReward);
            await _context.SaveChangesAsync();
            return Ok(newReward);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReward()
        {
            var rewards = await _context.Rewards.ToListAsync();
            return Ok(rewards);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReward(int id, [FromBody] RewardDto request)
        {
            var existingReward = await _context.Rewards.FindAsync(id);
            if (existingReward == null)
            {
                return NotFound("Không tìm thấy");
            }

            existingReward.ItemName = request.ItemName;
            existingReward.StockQuantity = request.StockQuantity;
            existingReward.PointsRequired = request.PointsRequired;

            await _context.SaveChangesAsync();
            return Ok(existingReward);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var existingReward = await _context.Rewards.FindAsync(id);
            if (existingReward == null)
            {
                return NotFound("Không tìm thấy");
            }
            _context.Rewards.Remove(existingReward);
            await _context.SaveChangesAsync();
            return Ok("Đã xóa");
        }
    }
}