using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;


namespace SportsLeague.DataAccess.Repositories;

public class RefereeRepository : GenericRepository<Referee>, IRefereeRepository
{
    public RefereeRepository(LeagueDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Referee>> GetByNationalityAsync(string nationality)
    {
        return await _dbSet// obtener arbitros por nacionalidad, le paso la nacionalidad,se ponen en minuscula y si es igual devuelvame un lista de arbitros de esa nacionalidad
            .Where(r => r.Nationality.ToLower() == nationality.ToLower())
            .ToListAsync();
    }
}

