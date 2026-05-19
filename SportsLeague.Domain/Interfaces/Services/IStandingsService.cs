namespace SportsLeague.Domain.Interfaces.Services;

public interface IStandingsService
{
    // keyword o palabra reservada "object"
    //se utiliza para indicar que el metodo puede devolver cualquier tipo de dato
    //se espera que el metodo devuleva un objeto que contenga la informacion de la tabla posiciones,
    //los maximos goleadores o las estadisticas de tarjetas, dependiendo del metodo.
    Task<object> GetStandingsAsync(int tournamentId);//obtener tabla de posiciones
    Task<object> GetTopScorersAsync(int tournamentId);//obtener lista de maximos goleadores
    Task<object> GetCardStatsAsync(int tournamentId);//obtener estadisticas de tarjetas de un torneo

    //el object es similar a t, es generico
}
