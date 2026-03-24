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

    }
}

