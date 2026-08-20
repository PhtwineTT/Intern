using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        private readonly IRewardServices _rewardServices;

        public RewardController(IRewardServices rewardServices)
        {
            _rewardServices = rewardServices;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRewards([FromQuery] QueryParameters queryParams)
        {
            var result = await _rewardServices.GetAllRewardsAsync(queryParams);
            return Ok(result);
        }

        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetRewardById(int id)
        {
            var result = await _rewardServices.GetRewardByIdAsync(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy" });
            return Ok(result);
        }

        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateReward([FromBody] RewardUpserDto request)
        {
            var result = await _rewardServices.CreateRewardAsync(request);
            return Ok(new { message = result });
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReward(int id, [FromBody] RewardUpserDto request)
        {
            var isSuccess = await _rewardServices.UpdateRewardAsync(id, request);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Cập nhật thành công" });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var isSuccess = await _rewardServices.DeleteRewardAsync(id);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Đã xóa" });
        }
    }
}