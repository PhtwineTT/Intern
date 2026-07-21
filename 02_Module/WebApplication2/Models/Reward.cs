namespace Module.Models
{
    public class Reward
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int PointsRequired { get; set; }
    }
}