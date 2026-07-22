using AuthAPI.Models.DTO;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardController(IRewardServices rewardServices) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRewards()
        {
            var rewards = await rewardServices.GetAllRewardAsync();
            return Ok(rewards);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetRewardById(int id)
        {
            var reward = await rewardServices.GetRewardByIdAsync(id);
            if (reward == null) return NotFound(new { message = "Không tìm thấy phần thưởng" }); // Trả về 404
            return Ok(reward);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddReward([FromBody] RewardDto request)
        {
            var result = await rewardServices.AddRewardAsync(request);
            if (result != "Success") return BadRequest(new { message = result });
            return Ok(new { message = "Thêm phần thưởng thành công" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReward(int id, [FromBody] RewardDto request)
        {
            var result = await rewardServices.UpdateRewardAsync(id, request);
            if (result == "Không tìm thấy phần thưởng") return NotFound(new { message = result });
            if (result != "Success") return BadRequest(new { message = result });
            return Ok(new { message = "Cập nhật phần thưởng thành công" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var result = await rewardServices.DeleteRewardAsync(id);
            if (result != "Success") return NotFound(new { message = result });
            return Ok(new { message = "Xóa phần thưởng thành công" });
        }
    }
}