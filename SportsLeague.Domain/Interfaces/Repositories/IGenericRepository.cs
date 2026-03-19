using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : AuditBase// <T> este resive una entidad o tipo de dato de tipo T, eso significa que recibe generico lo que sea. se conbierte en un objeto
{
    Task<IEnumerable<T>> GetAllAsync();//metodos genericos para utilizarlos dentro de la capa de acceso a datos
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
