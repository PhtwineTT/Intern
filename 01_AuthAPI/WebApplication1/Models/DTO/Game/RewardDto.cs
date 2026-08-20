using System.ComponentModel.DataAnnotations;
namespace AuthAPI.Models.DTO.Game
{
    public class RewardUpserDto
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        public string ItemName { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng còn lại không được âm")]
        public int StockQuantity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số điểm không được âm")]
        public int PointsRequired { get; set; }
        [Required(ErrorMessage = "Giải đấu không được để trống")]
        public int TournamentId { get; set; }
    }
    public class RewardReponseDto
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        public int PointsRequired { get; set; }
        public  int TournamentId { get; set; }
        public string? TournamentName { get; set; }
    }
    public static class RewardMappingExtensions
    {
        public static Reward ToEntity(this RewardUpserDto dto)
        {
            return new Reward
            {
                ItemName = dto.ItemName,
                StockQuantity = dto.StockQuantity,
                PointsRequired = dto.PointsRequired,
                TournamentId = dto.TournamentId,
            };
        }
        public static void UpdateEntity(this RewardUpserDto dto, Reward reward)
        {
            reward.ItemName = dto.ItemName;
            reward.StockQuantity = dto.StockQuantity;
            reward.PointsRequired = dto.PointsRequired;
            reward.TournamentId = dto.TournamentId;
        }
        public static RewardReponseDto ToReponseDto(this Reward reward)
        {
            return new RewardReponseDto
            {
                Id = reward.Id,
                ItemName = reward.ItemName,
                StockQuantity = reward.StockQuantity,
                PointsRequired = reward.PointsRequired,
                TournamentId = reward.TournamentId,
                TournamentName = reward.Tournament?.Name,
            };
        }
    }
}