using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{ // NOMBRE D EL CLASE: HERENCIA DE GENERIC, RECIBE OBJ TIPO PLAYER, Y GENERA LA INTERFAZ CON EL REPOSITORIO
    public PlayerRepository(LeagueDbContext context) : base(context)// CONSTRUCTOR VACIO
    {
    }

    public async Task<IEnumerable<Player>> GetByTeamAsync(int teamId)
    {
        return await _dbSet
            .Where(p => p.TeamId == teamId)
            .Include(p => p.Team)
            .ToListAsync();
    }

    public async Task<Player?> GetByTeamAndNumberAsync(int teamId, int number)// EL ID DEL EQUIPO Y EL NUMERO DE LA CAMISETA
    {
        return await _dbSet
            .Where(p => p.TeamId == teamId && p.Number == number)//PLAYER TAL QUE PLAYER LA PROPIEDAD ID, BUSQUELA QUE SEA IGUAL A LA QUE ME PAN POR PARAMETRO Y A SU VEZ QUE EL NUMERO SEA IGUAL AL QUE ME PASAN POR PARAMETRO
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Player>> GetAllWithTeamAsync()
    {
        return await _dbSet
            .Include(p => p.Team)//EL INCLUD ES PARA USAR PROPIEDADES DE NAVEGACION, DESDE JUGADOR HASTA EQUIPO
            .ToListAsync();
    }

    public async Task<Player?> GetByIdWithTeamAsync(int id)
    {
        return await _dbSet
            .Where (p => p.Id == id)
            .Include(p => p.Team)// INCLUYA AL EQUIPO DEL JUGDOR
            .FirstOrDefaultAsync();
    }
}
