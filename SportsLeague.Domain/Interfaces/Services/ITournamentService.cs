using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Interfaces.Services;

public interface ITournamentService
{
    Task<IEnumerable<Tournament>> GetAllAsync();
    Task<Tournament?> GetByIdAsync(int id);
    Task<Tournament> CreateAsync(Tournament tournament);
    Task UpdateAsync(int id, Tournament tournament);
    Task DeleteAsync(int id);
    Task UpdateStatusAsync(int id, TournamentStatus newStatus);//ACTUALIZAR TORNEO POR STATUS(PENDING, INPROGRESS..) 
    Task RegisterTeamAsync(int tournamentId, int teamId);//REGISTRA UN EQUIPO POR TORNEO
    Task<IEnumerable<Team>> GetTeamsByTournamentAsync(int tournamentId);//OBTENGO LOS EQUIPOS REGISTRADOS POR TORNEO
}
