using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : AuditBase
{
    protected readonly LeagueDbContext _context;// variables solo lectura sirven para inyectar la dependencia que se necesita en genericrepository 
    protected readonly DbSet<T> _dbSet;// manipulacion de mi set de datos, son las entidades o tablas 

    public GenericRepository(LeagueDbContext context)// metodo constructor
    {
        _context = context;// _context es la que se utiliza luego era como el this.
        _dbSet = context.Set<T>();// ayuda a hacer el crud basico: escribir, leer, actualizar, borrar
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();// este metodo savechangesasync () es crucial para que los cambios se reflejen en la base de datos.
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbSet.AnyAsync(e => e.Id == id);
    }
}
