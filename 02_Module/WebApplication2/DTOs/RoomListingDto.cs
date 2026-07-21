using System.ComponentModel.DataAnnotations;
namespace Module.DTOs
{
    public class RoomListingDto
    {
        [Required]
        public string Title { get; set; }
        public decimal Price { get; set; }
        public double Area { get; set; }
        public string Address { get; set; }
    }
}