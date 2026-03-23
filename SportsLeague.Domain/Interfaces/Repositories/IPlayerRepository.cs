using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface IPlayerRepository : IGenericRepository<Player>
    {
        Task<IEnumerable<Player>> GetByTeamAsync(int teamId);//ESTE METODO OBTENDRA TODOS LO JUGADORES POR EQUIPO
        Task<Player?> GetByTeamAndNumberAsync(int teamId, int number);//ESTE METODO OBTENDRA JUGADOR POR EQUIPO Y POR NOMBRE
        Task<IEnumerable<Player>> GetAllWithTeamAsync();//ESTE METODO OBTENDRA TODOS LO JUGADORES DE LOS EQUIPOS
        Task<Player?> GetByIdWithTeamAsync(int id);//ESTE METODO OBTENDRA JUGADOR POR ID
    }
}
