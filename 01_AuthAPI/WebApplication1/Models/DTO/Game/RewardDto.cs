namespace AuthAPI.Models.DTO.Game
{
    public class CreateRewardDto
    {
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int PointsRequired { get; set; }
    }
    public class RewardDto : CreateTeamDto
    {
        public int Id { get; set; }
    }
}