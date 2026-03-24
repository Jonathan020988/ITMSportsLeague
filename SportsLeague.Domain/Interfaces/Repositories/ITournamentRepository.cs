using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ITournamentRepository : IGenericRepository<Tournament>
{
    Task<IEnumerable<Tournament>> GetByStatusAsync(TournamentStatus status);//OBTENER TORNEOS POR ESTADO
    Task<Tournament?> GetByIdWithTeamsAsync(int id);//OBTENER TORNEO POR ID CON RELACION DE LOS EQUIPOS QUE HACEN PARTE DE ESE TORNEO
}
