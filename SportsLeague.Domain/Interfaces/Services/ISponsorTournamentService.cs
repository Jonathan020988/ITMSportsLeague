using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ISponsorTournamentService
    {
        Task LinkSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount);
        Task<IEnumerable<SponsorTournament>> GetByTournamentIdAsync(int tournamentId);
        Task<IEnumerable<SponsorTournament>> GetBySponsorIdAsync(int sponsorId);
        Task RemoveSponsorFromTournamentAsync(int sponsorId, int tournamentId);
    }
}
