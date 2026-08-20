namespace AuthAPI.Models
{
    public class CreateVenue
    {
        public string Name { get; set; } = string.Empty;
        public int TotalPCs { get; set; }
        public string HardwareSpecs { get; set; } = string.Empty;
    }
    public class Venue : CreateVenue
    {
        public int Id { get; set; }
    }
}
