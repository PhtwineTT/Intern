
using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Auth;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [Controller]
    public class VenueController : ControllerBase
    {
        private readonly IVenueServices _venueServices;
        public VenueController(IVenueServices venueServices)
        {
            _venueServices = venueServices;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllVenue([FromQuery] QueryParameters queryParams)
        {
            var result = await _venueServices.GetAllVenuesAsync(queryParams);
            return Ok(result);
        }
        [HttpGet("find/{id}")]
        public async Task<IActionResult> GetVenueById(int id)
        {
            var result = await _venueServices.GetVenueByIdAsync(id);
            if (result == null)
            {
                return NotFound(new { message = "Không tìm thấy" });
            }
            return Ok(result);
        }
        [HttpPost("create"), Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVenue([FromBody] VenueUpserDto request)
        {
            var result = await _venueServices.CreateVenueAsync(request);
            return Ok(new { message = "Đã tạo" });
        }
        [HttpPut("update/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVenue(int id, [FromBody] VenueUpserDto request)
        {
            var isSuccess = await _venueServices.UpdateVenueAsync(id, request);
            if (!isSuccess) return NotFound(new {message = "Không tìm thấy"});
            return Ok(new { message = "Đã cập nhật" });
        }
        [HttpDelete("delete/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVenue(int id)
        {
            var isSuccess = await _venueServices.DeleteVenueAsync(id);
            if (!isSuccess) return NotFound(new { message = "Không tìm thấy" });
            return Ok(new { message = "Đã xóa" });
        }
    }
}