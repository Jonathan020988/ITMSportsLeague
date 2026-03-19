using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    public TeamRepository(LeagueDbContext context) : base(context)
    {
    }

    public async Task<Team?> GetByNameAsync(string name)//aqui devuelvo un objeto de tipo TEAM
        //metodos de otencion de datos un por nombre y otro por ciudad
    {
        return await _dbSet
            .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());// busqueme las coincidencias en minusculas del nombre que le estoy pasando por parametro
    }

    public async Task<IEnumerable<Team>> GetByCityAsync(string city)//aqui devulevo una lista de objetos de tipo TEAM, lista de equipos por ciudad
    {
        return await _dbSet
            .Where(t => t.City.ToLower() == city.ToLower())
            .ToListAsync();
    }
}

