using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services;

public interface IRefereeService
{
    Task<IEnumerable<Referee>> GetAllAsync();// OBTENER TODOS LOS ARBITROS
    Task<Referee?> GetByIdAsync(int id);// ARBITRO POR ID
    Task<Referee> CreateAsync(Referee referee);// CREAR ARBITRO
    Task UpdateAsync(int id, Referee referee);// ACTUALIZAR ARBITRO
    Task DeleteAsync(int id);//ELIMINAR ARBITRO
}
