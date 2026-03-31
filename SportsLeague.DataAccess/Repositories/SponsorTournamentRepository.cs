using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class SponsorTournamentRepository : GenericRepository<SponsorTournament>, ISponsorTournamentRepository
    {
        

        public SponsorTournamentRepository(LeagueDbContext context) : base(context)// contructor vacio
        {           
        }

        public async Task<IEnumerable<SponsorTournament>> GetByTournamentIdAsync(int tournamentId)// trae todos los sponsors de un torneo
        {
            return await _dbSet
                .Where(st => st.TournamentId == tournamentId)
                .Include(st => st.Sponsor)
                .ToListAsync();
        }

        public async Task<IEnumerable<SponsorTournament>> GetBySponsorIdAsync(int sponsorId)// trae todos los torneos de un sponsor
        {
            return await _dbSet
                .Where(st => st.SponsorId == sponsorId)
                .Include(st => st.Tournament)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int sponsorId, int tournamentId)// verifica si ya existe la relacion
        {
            return await _dbSet
                .AnyAsync(st => st.SponsorId == sponsorId && st.TournamentId == tournamentId);
        }
    }
}