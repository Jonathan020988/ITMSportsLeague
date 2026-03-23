using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface IPlayerService// LA CAPA DE SERVICIOS NECESITA METODOS COMO CREATE UPDATE Y DELETE, POR QUE NO SON GENRICOS
{
    Task<IEnumerable<Player>> GetAllAsync();// TODOS LOS JUGADORES
    Task<Player?> GetByIdAsync(int id);// JUGADOR POR ID
    Task<IEnumerable<Player>> GetByTeamAsync(int teamId);// JUGADORES POR EQUIPO PASANDO AL ID
    Task<Player> CreateAsync(Player player);//CREO EL JUGADOR 
    Task UpdateAsync(int id, Player player);// SI EXISTE Y LUEGO ACTUALIZA
    Task DeleteAsync(int id);// BORRA EL JUGADOR, SI EXISTE
}

