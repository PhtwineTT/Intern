using System.ComponentModel.DataAnnotations;
namespace Module.DTOs
{
    public class UserDto
    {
        [Required]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
