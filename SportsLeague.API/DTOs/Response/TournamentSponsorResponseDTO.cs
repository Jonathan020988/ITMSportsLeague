namespace SportsLeague.API.DTOs.Response
{
    public class TournamentSponsorResponseDTO
    {
        public int Id { get; set; }
        public int SponsorId { get; set; }
        public int TournamentId { get; set; }
        public decimal ContractAmount { get; set; }
        public DateTime JoinedAt { get; set; }

        // se agrega esto para mas claridad de la informacion

        public string SponsorName { get; set; } = string.Empty;
        public string TournamentName { get; set; } = string.Empty;
    }
}
