namespace SportsLeague.Domain.Entities;

public class Team : AuditBase
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Stadium { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public DateTime FoundedDate { get; set; }

    // Navigation Property - Colección de jugadores
    public ICollection<Player> Players { get; set; } = new List<Player>();// con player, lista vacia de jugadores

    /*COLECCION UN ARRAY(UN ARREGLO)
    *ICOLLECTION, INSERTAR, CREAR, CONTAR, ORDENAR, ELIMINAR, AGREGAR REGISTROS NUEVOS (ES MUY LENTO POR TODO LO QUE HACE)
    *IENUMERABLE, SE ENFOCAN MAS EN PERFORMARCE, SON MEJORES PARA PETICIONES
    *IQUERABLE, SE ENFOCAN MAS EN PERFORMARCE, TRAER CONSULTAS  DE SQL EJ: SELECT  * FROM PLAYERS WHERE TEAMID= 2
    *ILIST,
    */
}
