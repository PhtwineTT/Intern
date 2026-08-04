using AuthAPi.Services.Interface;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Models.DTO.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto request)
        {
            var result = await _tournamentServices.CreateTournamentAsync(request);
            return Ok(new { message = result });
        }
        [HttpPut("update/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTournament(int id, [FromBody] CreateTournamentDto request)
        {
            var isSuccess = await _tournamentServices.UpdateTournamentAsync(id, request);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });

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
    }
}
