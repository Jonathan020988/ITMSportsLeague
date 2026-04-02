using AutoMapper;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Entities;

namespace SportsLeague.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Team mappings
        CreateMap<TeamRequestDTO, Team>();// del dto a la entidad
        CreateMap<Team, TeamResponseDTO>();// de la entidad al dto

        // Player mappings
        CreateMap<PlayerRequestDTO, Player>();
        CreateMap<Player, PlayerResponseDTO>()
            .ForMember(
            dest => dest.TeamName,
            opt => opt.MapFrom(src => src.Team.Name));//.formember genera una relacion entre player y team

        // Referee mappings
        CreateMap<RefereeRequestDTO, Referee>();
        CreateMap<Referee, RefereeResponseDTO>();

        // Tournament mappings
        CreateMap<TournamentRequestDTO, Tournament>();
        CreateMap<Tournament, TournamentResponseDTO>()
            .ForMember(
                dest => dest.TeamsCount,
                opt => opt.MapFrom(src =>
                    src.TournamentTeams != null ? src.TournamentTeams.Count : 0))
            .ForMember(
                dest => dest.SponsorsCount,
                opt => opt.MapFrom(src =>
                    src.TournamentSponsors != null ? src.TournamentSponsors.Count : 0));

        // Sponsor mappings
        CreateMap<Sponsor, SponsorResponseDTO>();
        CreateMap<SponsorRequestDTO, Sponsor>();

        //TournamentSponsor mappings
        CreateMap<TournamentSponsor, TournamentSponsorResponseDTO>()// se agregan datos para mas claridad en la informacion
            .ForMember(dest => dest.SponsorName,
                opt => opt.MapFrom(src => src.Sponsor.Name))
            .ForMember(dest => dest.TournamentName,
                opt => opt.MapFrom(src => src.Tournament.Name));
        CreateMap<TournamentSponsorRequestDTO, TournamentSponsor>();

    }


}

