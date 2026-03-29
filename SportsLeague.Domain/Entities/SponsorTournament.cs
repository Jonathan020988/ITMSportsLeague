namespace SportsLeague.Domain.Entities
{
    public class SponsorTournament : AuditBase
    {
        public int SponsorId { get; set; }  //(FK)
        public int TournamentId { get; set; }  //(FK)

        public decimal ContractAmount { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties, relacion hacia sponsor y relacion hacia tournament
        public Sponsor Sponsor { get; set; } = null!;
        public Tournament Tournament { get; set; } = null!;
    }
}
