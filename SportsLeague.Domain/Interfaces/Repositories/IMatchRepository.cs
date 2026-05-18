using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface IMatchRepository : IGenericRepository<Match>//herencia
{
    Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId);//obtener partidos por torneo
    Task<IEnumerable<Match>> GetByTeamAsync(int teamId);//obtener partidos por equipo
    Task<Match?> GetByIdWithDetailsAsync(int id);//obtener      partidos con detalles
    Task<IEnumerable<Match>> GetByTournamentWithDetailsAsync(int tournamentId);//obtener partidos por torneo con detalles
}
