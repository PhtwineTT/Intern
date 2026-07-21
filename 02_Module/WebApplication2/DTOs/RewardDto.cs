namespace Module.DTOs
{
    public class RewardDto
    {
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int PointsRequired { get; set; }
    }
}