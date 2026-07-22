using AuthAPI.Models.DTO;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubEventController(IClubEventServices eventServices) : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var events = await eventServices.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await eventServices.GetEventByIdAsync(id);
            if (ev == null) return NotFound(new { message = "Không tìm thấy sự kiện" });

            return Ok(ev);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ClubEventDto request)
        {
            var result = await eventServices.CreateEventAsync(request);
            if (result != "Success") return BadRequest(new { message = result });

            return Ok(new { message = "Tạo sự kiện thành công" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClubEventDto request)
        {
            var result = await eventServices.UpdateEventAsync(id, request);

            if (result == "Không tìm thấy sự kiện") return NotFound(new { message = result });
            if (result != "Success") return BadRequest(new { message = result });

            return Ok(new { message = "Cập nhật sự kiện thành công" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await eventServices.DeleteEventAsync(id);

            if (result != "Success") return NotFound(new { message = result });

            return Ok(new { message = "Xóa sự kiện thành công" });
        }
    }
}