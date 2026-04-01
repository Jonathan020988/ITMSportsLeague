using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface ITournamentSponsorService
    {
        Task LinkSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount);
        Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId);
        Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId);
        Task RemoveSponsorFromTournamentAsync(int sponsorId, int tournamentId);

        Task UnlinkSponsorFromTournamentAsync(int sponsorId, int tournamentId);// este metodo es para buscar las relciones de sponsor
    }
}
