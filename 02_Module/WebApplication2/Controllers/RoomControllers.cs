using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.Models;
using Module.DTOs;
namespace Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomListingDto request)
        {
            var room = new RoomListing
            {
                Title = request.Title,
                Price = request.Price,
                Area = request.Area,
                Address = request.Address,
                IsAvailable = true
            };
            _context.RoomListing.Add(room);
            await _context.SaveChangesAsync();
            return Ok(room);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _context.RoomListing.ToListAsync();
            return Ok(rooms);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomListingDto request)
        {
            var room = await _context.RoomListing.FindAsync(id);
            if (room == null)
            {
                return NotFound("Không tìm thấy");
            }
            room.Title = request.Title;
            room.Price = request.Price;
            room.Area = request.Area;
            room.Address = request.Address;
            await _context.SaveChangesAsync();
            return Ok(room);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.RoomListing.FindAsync(id);
            if (room == null)
            {
                return NotFound("Không tìm thấy");
            }
            _context.RoomListing.Remove(room);
            await _context.SaveChangesAsync();
            return Ok("Đã xóa");
        }
    }
}
