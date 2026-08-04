using System.ComponentModel.DataAnnotations;
namespace AuthAPI.Models.DTO
{
    public class CreateVenueDto
    {
        public string Name { get; set; } = string.Empty;
        public int ToltalPCs { get; set; }
        public string HardwareSpecs { get; set; } = string.Empty;
    }
    public class VenueDto : CreateVenueDto
    {
        public int Id { get; set; }
    }
}