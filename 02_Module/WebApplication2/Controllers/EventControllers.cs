using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.Models;
using Module.DTOs;
namespace Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        public EventControllers(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewEvent([FromBody] ClubEventDto request)
        {
            var newEvent = new ClubEvent
            {
                Name = request.Name,
                Theme = request.Theme,
                StartTime = request.StartTime,
                MaxAttendes = request.MaxAttendes,
                Location = request.Location,
            };
            _context.ClubEvents.Add(newEvent);
            await _context.SaveChangesAsync();
            return Ok(newEvent);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEvent()
        {
            var events = await _context.ClubEvents.ToListAsync();
            return Ok(events);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] ClubEventDto request)
        {
            var existingEvent = await _context.ClubEvents.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound("Không tìm thấy");
            }
            existingEvent.Name = request.Name;
            existingEvent.Theme = request.Theme;
            existingEvent.StartTime = request.StartTime;
            existingEvent.MaxAttendes = request.MaxAttendes;
            existingEvent.Location = request.Location;
            await _context.SaveChangesAsync();
            return Ok(existingEvent);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await _context.ClubEvents.FindAsync(id);
            if (existingEvent == null)
            {
                return NotFound("Không tìm thấy");
            }
            _context.ClubEvents.Remove(existingEvent); ;
            await _context.SaveChangesAsync();
            return Ok("Đã xóa");
        }
    }
}
