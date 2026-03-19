using Microsoft.EntityFrameworkCore;
using SportsLeague.Domain.Entities;

namespace SportsLeague.DataAccess.Context;

public class LeagueDbContext : DbContext// hereda de dbcontext que es una clase de EF
{
    public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
        : base(options)
    {
    }

    public DbSet<Team> Teams => Set<Team>();//entidad(Team), tabla(Teams) , entidad (Team); por cada tabla nueva o cada entridad es un nuevo DbSet

    protected override void OnModelCreating(ModelBuilder modelBuilder)// validaciones, que tipos de datos,longitud, si es o no obligatorio
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            entity.Property(t => t.City)
                  .IsRequired()// es obligatorio
                  .HasMaxLength(100);
            entity.Property(t => t.Stadium)
                  .HasMaxLength(150);
            entity.Property(t => t.LogoUrl)
                  .HasMaxLength(500);
            entity.Property(t => t.CreatedAt)
                  .IsRequired();
            entity.Property(t => t.UpdatedAt)
                  .IsRequired(false);// es obligatorio pero en el auditbase tiene ? lo que la vuelve nuleable por eso aca tiene el false 
            entity.HasIndex(t => t.Name)
                  .IsUnique();//columna es unica, no pueden haber nombres repetidos de equipo
        });
    }
}
