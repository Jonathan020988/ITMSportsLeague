using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ISponsorTournamentRepository : IGenericRepository<SponsorTournament>
    {
        Task<IEnumerable<SponsorTournament>> GetByTournamentIdAsync(int tournamentId);// trae todos los sponsors de un torneo
        Task<IEnumerable<SponsorTournament>> GetBySponsorIdAsync(int sponsorId);// trae todos los torneos de un sponsor
        Task<bool> ExistsAsync(int sponsorId, int tournamentId);// verifica si ya existe la relacion
    }
}
