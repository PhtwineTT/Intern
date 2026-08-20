using AuthAPi.Services.Interface;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentServices _tournamentServices;
        public TournamentController(ITournamentServices tournamentServices)
        {
            _tournamentServices = tournamentServices;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllTournaments([FromQuery] QueryParameters queryParams)
        {
            var tournaments = await _tournamentServices.GetAllTournamentAsync(queryParams);
            return Ok(tournaments);
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetTournamentById(int id)
        {
            var result = await _tournamentServices.GetTournamentByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
            return Ok(result);
        }
        [HttpPost("create")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateTournament([FromBody] TournamentUpserDto request)
        {
            var result = await _tournamentServices.CreateTournamentAsync(request);
            if (result.StartsWith("Lỗi"))
            {
                return BadRequest(new {message = result});
            }
            return Ok(new { message = result });
        }
        [HttpPut("update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] TournamentUpserDto request)
        {
            var isSuccess = await _tournamentServices.UpdateTournamentAsync(id, request);
            if (!isSuccess)
            {
                return BadRequest(new { message = "Cập nhật thất bại. Giải đấu không tồn tại hoặc địa điểm không hợp lệ." });
            }
            return Ok(new { message = "Cập nhật thành công!" });
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var isSuccess = await _tournamentServices.DeleteTournamentAsync(id);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Đã xóa" });
        }

        [HttpPost("{tournamentId}/register")]
        [Authorize(Roles = "Captain")]
        public async Task<IActionResult> RegisterTeam(int tournamentId, [FromBody] RegisterTournamentDto request)
        {
            var userIDClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIDClaim, out int currentUserId))
            {
                return Unauthorized(new { message = "Không xác minh được" });
            }
            var result = await _tournamentServices.RegisterTeamAsync(tournamentId, request, currentUserId);
            if (result.Contains("Thành công"))
            {
                return Ok(new { message = result });
            }
            return BadRequest(new { message = result });
        }

        [HttpPatch("resgistration/{registrationId}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateRegistrationStatus(int registrationId, [FromBody] UpdateRegistrationStatusDto request)
        {
            var result = await _tournamentServices.UpdateRegistrationStatusAsync(registrationId, request);
            if (result.Contains("Không tồn tại"))
            {
                return NotFound(new {message = result });
            }
            return Ok(new { message = result });
        }
    }
}
