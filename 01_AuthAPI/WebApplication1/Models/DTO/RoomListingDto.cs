using System.ComponentModel.DataAnnotations;
namespace AuthAPI.Models.DTO
{
    public class RoomListingDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Area { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}