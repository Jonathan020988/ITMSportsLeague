using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class TournamentSponsorService : ITournamentSponsorService
    {
        private readonly ITournamentSponsorRepository _tournamentSponsorRepository;
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public TournamentSponsorService(
            ITournamentSponsorRepository sponsorTournamentRepository,
            ISponsorRepository sponsorRepository,
            ITournamentRepository tournamentRepository)
        {
            _tournamentSponsorRepository = sponsorTournamentRepository;
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
            if (await _tournamentSponsorRepository.ExistsAsync(sponsorId, tournamentId))
                throw new InvalidOperationException("This sponsor is already linked to the tournament");

            // VALIDACIÓN 4: ContractAmount > 0
            if (contractAmount <= 0)
                throw new InvalidOperationException("Contract amount must be greater than 0");

            // CREAR RELACIÓN
            var TournamentSponsor = new TournamentSponsor
            {
                SponsorId = sponsorId,
                TournamentId = tournamentId,
                ContractAmount = contractAmount
            };

            await _tournamentSponsorRepository.CreateAsync(TournamentSponsor);
        }

        // 2. LISTAR SPONSORS DE UN TORNEO
        public async Task<IEnumerable<TournamentSponsor>> GetByTournamentIdAsync(int tournamentId)
        {
            return await _tournamentSponsorRepository.GetByTournamentIdAsync(tournamentId);
        }

        // 3. LISTAR TORNEOS DE UN SPONSOR
        public async Task<IEnumerable<TournamentSponsor>> GetBySponsorIdAsync(int sponsorId)
        {
            return await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);
        }

        // 4. ELIMINAR RELACIÓN
        public async Task RemoveSponsorFromTournamentAsync(int sponsorId, int tournamentId)
        {
            // VALIDAR QUE EXISTE
            var exists = await _tournamentSponsorRepository.ExistsAsync(sponsorId, tournamentId);

            if (!exists)
                throw new KeyNotFoundException("Relationship not found");

            // BUSCAR RELACIÓN
            var relations = await _tournamentSponsorRepository.GetBySponsorIdAsync(sponsorId);

            var relation = relations.FirstOrDefault(r => r.TournamentId == tournamentId);

            if (relation == null)
                throw new KeyNotFoundException("Relationship not found");

            await _tournamentSponsorRepository.DeleteAsync(relation.Id);
        }

        public async Task UnlinkSponsorFromTournamentAsync(int sponsorId, int tournamentId)// este es el metodo para buscar las relaciones de sponsor, filtra por tournamentid, sino existe error y si exixte lo elimina
        {
            // Buscar la relación
            var relation = await _tournamentSponsorRepository
                .GetBySponsorIdAsync(sponsorId);

            var existing = relation
                .FirstOrDefault(x => x.TournamentId == tournamentId);

            if (existing == null)
                throw new KeyNotFoundException("Relation between sponsor and tournament not found");

            await _tournamentSponsorRepository.DeleteAsync(existing.Id);
        }
    }
}
