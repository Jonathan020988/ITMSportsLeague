using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ITournamentTeamRepository : IGenericRepository<TournamentTeam>
{
    Task<TournamentTeam?> GetByTournamentAndTeamAsync(int tournamentId, int teamId);//METODO PARA OBTENER UNA RELACION ESPECIFICA ENTRE UN TORNEO Y UN EQUIPO
    Task<IEnumerable<TournamentTeam>> GetByTournamentAsync(int tournamentId);//METODO PARA OBTENER TODAS LAS RELACIONES DE EQUIPOS EN UN TORNEO ESPECIFICO
  
}

