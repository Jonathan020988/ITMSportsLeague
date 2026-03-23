using SportsLeague.Domain.Enums;

namespace SportsLeague.Domain.Entities;

public class Player : AuditBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Number { get; set; }
    public PlayerPosition Position { get; set; }

    // Foreign Key
    public int TeamId { get; set; }// para relacionarla con el equipo

    // Navigation Property
    public Team Team { get; set; } = null!;//propiedad de navegacion es un obejto unico; un jugador pertenece a un solo equipo; 
}                                   //SE INICIALIZA NULA, PARA EVITAR PROBLEMAS
