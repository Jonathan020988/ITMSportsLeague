using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface ITeamService
{
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(int id);// recibe el id, un equipo por id
    Task<Team> CreateAsync(Team team);//recibe el equipo que voy a crear
    Task UpdateAsync(int id, Team team);//recibe el id y el cuerpo del que voy a actualizar
    Task DeleteAsync(int id);// solo el id porque lo boorra en la base de datos con eso
}
