using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Security;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamServices _teamServices;
        private readonly IFileUploadServices _fileUploadServices;
        public TeamController(ITeamServices teamServices, IFileUploadServices fileUploadServices)
        {
            _teamServices = teamServices;
            _fileUploadServices = fileUploadServices;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTeams([FromQuery] QueryParameters queryParams)
        {
            var result = await _teamServices.GetAllTeamsAsync(queryParams);
            return Ok(result);
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            var result = await _teamServices.GetTeamByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
            return Ok(result);
        }
        [HttpPost("create"), Authorize]
        public async Task<IActionResult> CreateTeam([FromBody] TeamUpserDto request)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(!int.TryParse(userIdClaim, out var currentUserId))
            {
                return Unauthorized(new {message = "Không tìm thấy"});
            }
            var result = await _teamServices.CreateTeamAsync(currentUserId, request);
            return Ok(new
            {
                message = result,
                note = "Nếu bạn vừa được thăng cấp lên Captain"
            });
        }
        [HttpPut("update/{id}")]
        [Authorize(Roles = Roles.Captain)]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] TeamUpserDto request)
        {
            var isSuccess = await _teamServices.UpdateTeamAsync(id, request);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Cập nhật thành công" });
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Roles.Captain)]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var isSuccess = await _teamServices.DeleteTeamAsync(id);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Đã xóa" });
        }
        [HttpPost("{id}/upload-logo"), Authorize(Roles = Roles.Captain)]
        public async Task<IActionResult> UploadTeamLogo(int id, IFormFile file)
        {
            string imageUrl = await _fileUploadServices.UploadFileAsync(file, "TeamLogo");
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("Lỗi khi tải ảnh");
            }
            bool isUpdated = await _teamServices.UpdateTeamLogoAsync(id, imageUrl);
            if (!isUpdated) return NotFound("Không tìm thấy ID này");
            return Ok(new { Url = imageUrl, Message = "Đã cập nhật thành công" });
        }
    }
}
