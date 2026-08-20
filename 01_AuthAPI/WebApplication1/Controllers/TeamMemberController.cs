using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Security;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/team-members")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberServices _teamMemberServices;

        public TeamMemberController(ITeamMemberServices teamMemberServices)
        {
            _teamMemberServices = teamMemberServices;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTeamMembers([FromQuery] QueryParameters queryParams)
        {
            var result = await _teamMemberServices.GetAllMembersAsync(queryParams);
            return Ok(result);
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            var result = await _teamMemberServices.GetMemberByIdAsync(id);
            if (result == null) return NotFound(new { message = "Không tìm thấy" });
            return Ok(result);
        }
        [HttpPost("create")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateTeamMember([FromBody] TeamMemberUpserDto request)
        {
            var result = await _teamMemberServices.CreateMemberAsync(request);
            return Ok(new { message = result });
        }
        [HttpPut("update/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] TeamMemberUpserDto request)
        {
            var isSuccess = await _teamMemberServices.UpdateMemberAsync(id, request);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            
            return Ok(new { message = "Đã cập nhật" });
        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var isSuccess = await _teamMemberServices.DeleteMemberAsync(id);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Đã xóa" });
        }
    }
}