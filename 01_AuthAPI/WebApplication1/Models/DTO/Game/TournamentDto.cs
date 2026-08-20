using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.DTO.Game
{
    public class TournamentUpserDto
    {
        [Required(ErrorMessage = "Tên không được trống")]
        public string Name { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int MaxTeams { get; set; }
        public decimal PrizePool { get; set; }
        public int? VenueId { get; set; }
        public TournamentStatus Status { get; set; } = TournamentStatus.Upcoming;
    }
    public class TournamentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int MaxTeams { get; set; }
        public decimal PrizePool { get; set; }
        public int? VenueId { get; set;}
        public string? VenueName { get; set; }
        public TournamentStatus Status { get; set;}
        public int TotalTeams { get; set; }
    }
    public static class TournamentMapping
    {
        public static Tournament ToEntity(this TournamentUpserDto dto)
        {
            return new Tournament
            {
                Name = dto.Name,
                Format = dto.Format,
                MaxTeams = dto.MaxTeams,
                PrizePool = dto.PrizePool,
                VenueId = dto.VenueId,
                Status = dto.Status,
            };
        }
        public static void UpdateEntity(this TournamentUpserDto dto, Tournament tournament)
        {
            tournament.Name = dto.Name;
            tournament.Format = dto.Format; 
            tournament.MaxTeams = dto.MaxTeams;
            tournament.PrizePool = dto.PrizePool;
            tournament.VenueId = dto.VenueId;
            tournament.Status = dto.Status;
        }
        public static TournamentResponseDto ToResponseDto(this Tournament tournament)
        {
            return new TournamentResponseDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                Format = tournament.Format,
                MaxTeams = tournament.MaxTeams,
                PrizePool = tournament.PrizePool,
                VenueId = tournament.VenueId,
                VenueName = tournament.Venue?.Name,
                Status = tournament.Status,
                TotalTeams = tournament.Teams?.Count ?? 0
            };
        }
    }
}
