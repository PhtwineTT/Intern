using AuthAPI.Models.DTO;
using AuthAPI.Services;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomListingController(IRoomListingServices roomService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await roomService.GetAllRoomAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var room = await roomService.GetRoomByIdAsync(id);
            return room != null ? Ok(room) : NotFound(new { message = "Không tìm thấy phòng" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RoomListingDto request)
        {
            var result = await roomService.CreateRoomAsync(request);
            return Ok(new { message = result });
        }
    }
}