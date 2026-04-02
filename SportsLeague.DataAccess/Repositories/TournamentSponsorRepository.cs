using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class TournamentSponsorRepository : GenericRepository<TournamentSponsor>, ITournamentSponsorRepository
    {
        

        public TournamentSponsorRepository(LeagueDbContext context) : base(context)// contructor vacio
        {           
        }

        public async Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId)// trae todos los sponsors de un torneo
        {
            return await _dbSet
                .Where(ts => ts.TournamentId == tournamentId)
                .Include(ts => ts.Sponsor)
                .Include(ts => ts.Tournament)
                .ToListAsync();
        }

        public async Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId)// trae todos los torneos de un sponsor
        {
            return await _dbSet
                .Where(ts => ts.SponsorId == sponsorId)
                .Include(ts => ts.Tournament)
                .Include(ts => ts.Sponsor)// lo añadi despues de hacer pruebas en swagger por que me estaba devolviendo null en el nombre del sponsor
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int sponsorId, int tournamentId)// verifica si ya existe la relacion
        {
            return await _dbSet
                .AnyAsync(ts => ts.SponsorId == sponsorId && ts.TournamentId == tournamentId);
        }
    }
}