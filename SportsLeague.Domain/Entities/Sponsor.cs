using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities
{
    public class Sponsor : AuditBase
    {
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? WebsiteUrl { get; set; }

        //  enum category
        public SponsorCategory Category { get; set; }

        // propiedad de navegacion
        public ICollection<SponsorTournament> SponsorTournaments { get; set; } = new List<SponsorTournament>();
    }
}
