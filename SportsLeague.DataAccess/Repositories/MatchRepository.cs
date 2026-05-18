using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class MatchRepository : GenericRepository<Match>, IMatchRepository
{
    public MatchRepository(LeagueDbContext context) : base(context)//metodo por defecto sin sobrecarga
    {
    }

    public async Task<IEnumerable<Match>> GetByTournamentAsync(int tournamentId)//metodo para obtener partido por torneo
    {
        return await _dbSet
            .Where(m => m.TournamentId == tournamentId)//le paso el id, haho el whhhhere que es el filtro o la condicion qq uno sea igual al otro
            .OrderBy(m => m.Matchday)//ordenemelo por la jornada
            .ThenBy(m => m.MatchDate)//ordeneme la lista por la fecha
            .ToListAsync();
    }

    public async Task<IEnumerable<Match>> GetByTeamAsync(int teamId)// obtener partido por equipo
    {
        return await _dbSet
            .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)//o
            .Include(m => m.HomeTeam)//equipo local
            //.ThenInclude(ht => ht.Players)//informmacion de los jugadores del equipo local
            .Include(m => m.AwayTeam)//equipo visitante
            //.ThenInclude(ht => ht.Players)
            .OrderBy(m => m.MatchDate)
            .ToListAsync();
    }

    public async Task<Match?> GetByIdWithDetailsAsync(int id)//partidos por id con detalle
    {
        return await _dbSet
            .Where(m => m.Id == id)
            .Include(m => m.Tournament)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Referee)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Match>> GetByTournamentWithDetailsAsync(int tournamentId)//obtener los partidos por torneo con detalle
    {
        return await _dbSet
            .Where(m => m.TournamentId == tournamentId)
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.Referee)
            .OrderBy(m => m.Matchday)
            .ThenBy(m => m.MatchDate)
            .ToListAsync();
    }
}
