using System.ComponentModel.DataAnnotations;
namespace Module.DTOs
{
    public class ClubEventDto
    {
        [Required]
        public string Name { get; set; }
        public string Theme { get; set; }
        public DateTime StartTime { get; set; }
        public int MaxAttendes { get; set; }
        public string Location { get; set; }
    }
}
