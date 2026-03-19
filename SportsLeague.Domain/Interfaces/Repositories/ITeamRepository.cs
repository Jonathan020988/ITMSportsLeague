using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories;

public interface ITeamRepository : IGenericRepository<Team>// ITeamRepository el hereda de la interfaz IGenericRepository y este recibe un objeto de tipo team
{
    Task<Team?> GetByNameAsync(string name);// el asyn es por que son metodos asincronicos
    Task<IEnumerable<Team>> GetByCityAsync(string city);
}
