
using AutoMapper;
using AuthAPI.Models;
using AuthAPI.Models.DTO;
using AuthAPI.Models.DTO.Game;
using AuthAPI.Models.DTO.Auth;
namespace AuthAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<RegisterDto, User>();

            CreateMap<Tournament, TournamentDto>().ForMember(dest => dest.GameTitle, opt => opt.MapFrom(src => src.GameTitle.ToString()));
            CreateMap<CreateTournamentDto, Tournament>();

            CreateMap<Team, TeamDto>();
            CreateMap<CreateTeamDto, Team>();

            CreateMap<TeamMember, TeamMemberDto>();
            CreateMap<CreateTeamMemberDto, TeamMember>();
            
            CreateMap<Venue, VenueDto>();
            CreateMap<CreateVenueDto, Venue>();

            CreateMap<Reward, RewardDto>();
            CreateMap<CreateRewardDto, Reward>();

            CreateMap<Venue, VenueBasicDto>();
            CreateMap<Team, TeamBasicDto>();
        }
    }
}