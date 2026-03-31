namespace SportsLeague.API.DTOs.Response
{
    public class SponsorTournamentResponseDTO
    {
        public int Id { get; set; }
        public int SponsorId { get; set; }
        public int TournamentId { get; set; }
        public decimal ContractAmount { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
