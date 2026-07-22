namespace AuthAPI.Models.DTO
{
    public class RewardDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int PointsRequired { get; set; }
    }
}