namespace SportsLeague.Domain.Entities;

public class TournamentTeam : AuditBase
{
    public int TournamentId { get; set; }//FK
    public int TeamId { get; set; }//FK
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;// POR QUE NO ESTA EN EL AUDITBASE, PORQUE NO TODAS LA ENTIDADES TIENEN FECHA DE REGISTRO
    // NO TODAS LAS TABLAS LA DEBEN TENER, AUDIT BASE CREAR Y ACTUALIZAR PERO EL REGISTRO NO ES PARA TODO

    // Navigation Properties
    public Tournament Tournament { get; set; } = null!;
    public Team Team { get; set; } = null!;
}
