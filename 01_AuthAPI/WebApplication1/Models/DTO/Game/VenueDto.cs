using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;
namespace AuthAPI.Models.DTO
{
    public class VenueUpserDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = string.Empty;
        [Range(1, int.MaxValue, ErrorMessage = "Số máy phải lớn hơn 0")]
        public int TotalPCs { get; set; }
        public string HardwareSpecs { get; set; } = string.Empty;
    }
    public class VenueResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TotalPCs { get; set; }
        public string HardwareSpecs { get; set; } = string.Empty;
    }
    public static class VenueMapping
    {
        public static Venue ToEntity(this VenueUpserDto dto)
        {
            return new Venue
            {
                Name = dto.Name,
                TotalPCs = dto.TotalPCs,
                HardwareSpecs = dto.HardwareSpecs
            };
        }
        public static void UpdateEntity(this VenueUpserDto dto, Venue venue)
        {
            venue.Name = dto.Name;
            venue.TotalPCs = dto.TotalPCs;
            venue.HardwareSpecs = dto.HardwareSpecs;
        }
        public static VenueResponseDto ToResponseDto (this  Venue venue)
        {
            return new VenueResponseDto
            {
                Id = venue.Id,
                Name = venue.Name,
                TotalPCs = venue.TotalPCs,
                HardwareSpecs = venue.HardwareSpecs,
            };
        }
    }
}