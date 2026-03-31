using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class SponsorTournamentService : ISponsorTournamentService
    {
        private readonly ISponsorTournamentRepository _sponsorTournamentRepository;
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public SponsorTournamentService(
            ISponsorTournamentRepository sponsorTournamentRepository,
            ISponsorRepository sponsorRepository,
            ITournamentRepository tournamentRepository)
        {
            _sponsorTournamentRepository = sponsorTournamentRepository;
            _sponsorRepository = sponsorRepository;
            _tournamentRepository = tournamentRepository;
        }

        // 1. VINCULAR SPONSOR A TORNEO
        public async Task LinkSponsorToTournamentAsync(int sponsorId, int tournamentId, decimal contractAmount)
        {
            // VALIDACIÓN 1: Sponsor existe
            var sponsor = await _sponsorRepository.GetByIdAsync(sponsorId);
            if (sponsor == null)
                throw new KeyNotFoundException("Sponsor not found");

            // VALIDACIÓN 2: Tournament existe
            var tournament = await _tournamentRepository.GetByIdAsync(tournamentId);
            if (tournament == null)
                throw new KeyNotFoundException("Tournament not found");

            // VALIDACIÓN 3: No duplicar relación
            if (await _sponsorTournamentRepository.ExistsAsync(sponsorId, tournamentId))
                throw new InvalidOperationException("This sponsor is already linked to the tournament");

            // VALIDACIÓN 4: ContractAmount > 0
            if (contractAmount <= 0)
                throw new InvalidOperationException("Contract amount must be greater than 0");

            // CREAR RELACIÓN
            var sponsorTournament = new SponsorTournament
            {
                SponsorId = sponsorId,
                TournamentId = tournamentId,
                ContractAmount = contractAmount
            };

            await _sponsorTournamentRepository.CreateAsync(sponsorTournament);
        }

        // 2. LISTAR SPONSORS DE UN TORNEO
        public async Task<IEnumerable<SponsorTournament>> GetByTournamentIdAsync(int tournamentId)
        {
            return await _sponsorTournamentRepository.GetByTournamentIdAsync(tournamentId);
        }

        // 3. LISTAR TORNEOS DE UN SPONSOR
        public async Task<IEnumerable<SponsorTournament>> GetBySponsorIdAsync(int sponsorId)
        {
            return await _sponsorTournamentRepository.GetBySponsorIdAsync(sponsorId);
        }

        // 4. ELIMINAR RELACIÓN
        public async Task RemoveSponsorFromTournamentAsync(int sponsorId, int tournamentId)
        {
            // VALIDAR QUE EXISTE
            var exists = await _sponsorTournamentRepository.ExistsAsync(sponsorId, tournamentId);

            if (!exists)
                throw new KeyNotFoundException("Relationship not found");

            // BUSCAR RELACIÓN
            var relations = await _sponsorTournamentRepository.GetBySponsorIdAsync(sponsorId);

            var relation = relations.FirstOrDefault(r => r.TournamentId == tournamentId);

            if (relation == null)
                throw new KeyNotFoundException("Relationship not found");

            await _sponsorTournamentRepository.DeleteAsync(relation.Id);
        }
    }
}
