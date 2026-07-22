using Microsoft.EntityFrameworkCore;
namespace AuthAPI.Models
{
    public class RoomListing
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        [Precision (18,2)]
        public decimal Price { get; set; }
        public double Area { get; set; }
        public string Address { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
    }
}
